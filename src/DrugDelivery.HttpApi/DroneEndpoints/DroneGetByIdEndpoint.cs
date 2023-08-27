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
/// Get a Drone by Id
/// </summary>
public class DroneGetByIdEndpoint : IEndpoint<IResult, GetByIdDroneRequest, IRepository<Drone>>
{
    private readonly IMapper _mapper;
    public DroneGetByIdEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/drones/{droneId}",
            async (Guid droneId, IRepository<Drone> droneRepository) =>
            {
                return await HandleAsync(new GetByIdDroneRequest(droneId), droneRepository);
            })
            .Produces<GetByIdDroneResponse>()
            .WithTags("DroneEndpoints");
    }

    public async Task<IResult> HandleAsync(GetByIdDroneRequest request, IRepository<Drone> droneRepository)
    {
        var response = new GetByIdDroneResponse(request.CorrelationId());

        var drone = await droneRepository.GetByIdAsync(request.DroneId);
        if (drone is null)
            return Results.NotFound();

        var dto = _mapper.Map<DroneDto>(drone);
        response.Drone = dto;
        return Results.Ok(response);
    }
}
