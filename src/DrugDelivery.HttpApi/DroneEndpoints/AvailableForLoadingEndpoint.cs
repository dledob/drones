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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Endpoint;

namespace Drugdelivery.HttpApi.DroneEndpoints;

/// <summary>
/// Drones available for loading
/// </summary>
public class AvailableForLoadingEndpoint : IEndpoint<IResult, IRepository<Drone>>
{
    private readonly IMapper _mapper;
    public AvailableForLoadingEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/drones/available-for-loading",
            async (IRepository<Drone> itemRepository) =>
            {
                return await HandleAsync(itemRepository);
            })
            .Produces<AvailableForLoadingResponse>()
            .WithTags("DroneEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<Drone> droneRepository)
    {
        var response = new AvailableForLoadingResponse();

        var spec = new DronesAvailableForLoadingSpecification();
        var drones = await droneRepository.ListAsync(spec);

        response.Drones = new List<DroneDto>();
        if(drones != null)
        {
            response.Drones.AddRange(drones.Select(drone => _mapper.Map<DroneDto>(drone)));
        }

        return Results.Ok(response);
    }
}
