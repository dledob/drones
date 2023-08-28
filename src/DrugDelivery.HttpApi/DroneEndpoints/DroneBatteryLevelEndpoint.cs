using System;
using System.Threading.Tasks;
using AutoMapper;
using DrugDelivery.Core.Entities;
using DrugDelivery.Core.Interfaces;
using DrugDelivery.HttpApi.DroneEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace Drugdelivery.HttpApi.DroneEndpoints;

/// <summary>
/// Get Drone Battery Level
/// </summary>
public class DroneBatteryLevelEndpoint : IEndpoint<IResult, DroneBatteryLevelEndpointRequest, IRepository<Drone>>
{
    public DroneBatteryLevelEndpoint()
    {
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/drones/{droneId}/battery-level",
            async (Guid droneId, IRepository<Drone> droneRepository) =>
            {
                return await HandleAsync(new DroneBatteryLevelEndpointRequest(droneId), droneRepository);
            })
            .Produces<DroneBatteryLevelEndpointResponse>()
            .WithTags("DroneEndpoints");
    }

    public async Task<IResult> HandleAsync(DroneBatteryLevelEndpointRequest request, IRepository<Drone> droneRepository)
    {
        var response = new DroneBatteryLevelEndpointResponse(request.CorrelationId());

        var drone = await droneRepository.GetByIdAsync(request.DroneId);
        if (drone is null)
            return Results.NotFound();
        
        response.BatteryLevel = drone.BatteryCapacity;
        return Results.Ok(response);
    }
}
