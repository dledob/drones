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
/// Register a new Drone
/// </summary>
public class RegisterDroneEndpoint : IEndpoint<IResult, RegisterDroneRequest, IRepository<Drone>>
{
    private readonly IMapper _mapper;
    public RegisterDroneEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/drones",
            //[Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (RegisterDroneRequest request, IRepository<Drone> itemRepository) =>
            {
                return await HandleAsync(request, itemRepository);
            })
            .Produces<RegisterDroneResponse>()
            .WithTags("DroneEndpoints");
    }

    public async Task<IResult> HandleAsync(RegisterDroneRequest request, IRepository<Drone> droneRepository)
    {
        var response = new RegisterDroneResponse(request.CorrelationId());

        var droneBySerialNumberSpecification = new DroneBySerialNumberSpecification(request.SerialNumber);
        var existingDrone = await droneRepository.CountAsync(droneBySerialNumberSpecification);
        if (existingDrone > 0)
        {
            throw new DuplicateException($"A drone with serial number {request.SerialNumber} already exists");
        }

        var newDrone = await droneRepository.AddAsync(new Drone()
        {
            SerialNumber = request.SerialNumber,
            Model = request.Model,
            BatteryCapacity = request.BatteryCapacity,
            State = DroneState.IDLE,
            WeightLimit = request.WeightLimit
        });
        
        var dto = _mapper.Map<DroneDto>(newDrone);
        response.Drone = dto;
        return Results.Created($"api/drones/{dto.Id}", response);
    }
}
