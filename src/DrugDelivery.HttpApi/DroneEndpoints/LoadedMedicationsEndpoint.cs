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
/// Loaded Medications for a Drone
/// </summary>
public class LoadedMedicationsEndpoint : IEndpoint<IResult, Guid, IRepository<Drone>>
{
    private readonly IMapper _mapper;
    public LoadedMedicationsEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/drones/{droneId}/loaded-medications",
            async (Guid droneId, IRepository<Drone> itemRepository) =>
            {
                return await HandleAsync(droneId, itemRepository);
            })
            .Produces<LoadedMedicationsResponse>()
            .WithTags("DroneEndpoints");
    }

    public async Task<IResult> HandleAsync(Guid droneId, IRepository<Drone> droneRepository)
    {
        var response = new LoadedMedicationsResponse();

        var spec = new DroneWithMedicationsSpecification(droneId);
        var drone = await droneRepository.FirstOrDefaultAsync(spec);
        if (drone is null)
            return Results.NotFound();

        response.Medications = new List<MedicationDto>();
        foreach(var item in drone.LoadedMedications) 
        {
            response.Medications.Add(_mapper.Map<MedicationDto>(item.Medication));
        }

        return Results.Ok(response);
    }
}
