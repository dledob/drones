using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DrugDelivery.Core.Constants;
using DrugDelivery.Core.Entities;
using DrugDelivery.Core.Exceptions;
using DrugDelivery.Core.Interfaces;
using DrugDelivery.Core.Specifications;
using DrugDelivery.HttpApi.DroneEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace Drugdelivery.HttpApi.DroneEndpoints;

/// <summary>
/// Loading a Drone
/// </summary>
public class LoadingDroneEndpoint : IEndpoint<IResult, Guid, LoadingDroneRequest, IRepository<Drone>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Medication> _medicationRepository;
    private readonly IRepository<LoadedMedication> _loadedMedicationRepository;
    public LoadingDroneEndpoint(IMapper mapper, IRepository<Medication> medicationRepository, IRepository<LoadedMedication> loadedMedicationRepository)
    {
        _mapper = mapper;
        _medicationRepository = medicationRepository;
        _loadedMedicationRepository = loadedMedicationRepository;

    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/drones/{droneId}/loading",
            //[Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (Guid droneId, LoadingDroneRequest request, IRepository<Drone> itemRepository) =>
            {
                return await HandleAsync(droneId, request, itemRepository);
            })
            .Produces<LoadingDroneResponse>()
            .WithTags("DroneEndpoints");
    }

    public async Task<IResult> HandleAsync(Guid droneId, LoadingDroneRequest request, IRepository<Drone> droneRepository)
    {
        var response = new LoadingDroneResponse(request.CorrelationId());

        var drone = await droneRepository.GetByIdAsync(droneId);
        if (drone is null)
            return Results.NotFound();

        if(drone.State != DroneState.IDLE)
        {
            throw new DrugDeliveryException("Drone cannot be in 'LOADING' state", 101);
        }

        if (drone.BatteryCapacity < 25)
        {
            throw new DrugDeliveryException("Drone battery level is below 25%", 102);
        }

        var totalWeight = request.Medications.Sum(e => e.Weight);
        if (drone.WeightLimit < totalWeight)
        {
            throw new DrugDeliveryException($"Drone weight limit is {drone.WeightLimit}", 103);
        }

        drone.State = DroneState.LOADING;
        await droneRepository.UpdateAsync(drone);
        
        foreach(var item in request.Medications)
        {
            var medication = await _medicationRepository.AddAsync(new Medication
            {
                Code = item.Code,
                Name = item.Name,
                Weight = item.Weight,
                Image = item.Image
            });
            await _loadedMedicationRepository.AddAsync(new LoadedMedication
            {
                MedicationId = medication.Id,
                DroneId = drone.Id
            });
        }

        drone.State = DroneState.LOADED;
        await droneRepository.UpdateAsync(drone);
        return Results.Ok(response);
    }
}
