using System;
using System.Linq;
using System.Threading.Tasks;
using DrugDelivery.Core.Constants;
using DrugDelivery.Core.Entities;
using DrugDelivery.Core.Exceptions;
using DrugDelivery.Core.Interfaces;
using FluentValidation;
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
    private readonly IRepository<Medication> _medicationRepository;
    private readonly IRepository<LoadedMedication> _loadedMedicationRepository;
    private readonly IValidator<LoadingDroneRequest> _validator;
    public LoadingDroneEndpoint(IRepository<Medication> medicationRepository, IRepository<LoadedMedication> loadedMedicationRepository,
        IValidator<LoadingDroneRequest> validator)
    {
        _medicationRepository = medicationRepository;
        _loadedMedicationRepository = loadedMedicationRepository;
        _validator = validator;

    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/drones/{droneId}/loading",
            [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
        {
            var firstError = validationResult.Errors.First();
            throw new ValidateModelException(firstError.ErrorMessage, firstError.ErrorCode);
        }

        var drone = await droneRepository.GetByIdAsync(droneId);
        if (drone is null)
            return Results.NotFound();

        if(drone.State != DroneState.IDLE)
        {
            throw new DrugDeliveryException("Drone cannot be in 'LOADING' state", "101");
        }

        if (drone.BatteryCapacity < 25)
        {
            throw new DrugDeliveryException("Drone battery level is below 25%", "102");
        }

        var totalWeight = request.Medications.Sum(e => e.Weight);
        if (drone.WeightLimit < totalWeight)
        {
            throw new DrugDeliveryException($"Drone weight limit is {drone.WeightLimit}", "103");
        }

        drone.State = DroneState.LOADING;
        await droneRepository.UpdateAsync(drone);
        
        foreach(var item in request.Medications)
        {
            var medication = await _medicationRepository.AddAsync(new Medication(name: item.Name, code: item.Code, weight: item.Weight, image: item.Image));
            await _loadedMedicationRepository.AddAsync(new LoadedMedication(droneId: drone.Id, medicationId: medication.Id));
        }

        drone.State = DroneState.LOADED;
        await droneRepository.UpdateAsync(drone);
        return Results.Ok(response);
    }
}
