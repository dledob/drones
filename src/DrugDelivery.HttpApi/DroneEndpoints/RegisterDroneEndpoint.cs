using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DrugDelivery.Core.Constants;
using DrugDelivery.Core.Entities;
using DrugDelivery.Core.Exceptions;
using DrugDelivery.Core.Interfaces;
using DrugDelivery.Core.Specifications;
using DrugDelivery.HttpApi.DroneEndpoints;
using FluentValidation;
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
    private readonly IValidator<RegisterDroneRequest> _validator;
    public RegisterDroneEndpoint(IMapper mapper, IValidator<RegisterDroneRequest> validator)
    {
        _mapper = mapper;
        _validator = validator;
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

        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
        {
            var firstError = validationResult.Errors.First();
            throw new ValidateModelException(firstError.ErrorMessage, firstError.ErrorCode);
        }

        var droneBySerialNumberSpecification = new DroneBySerialNumberSpecification(request.SerialNumber);
        var existingDrone = await droneRepository.CountAsync(droneBySerialNumberSpecification);
        if (existingDrone > 0)
        {
            throw new DuplicateException($"A drone with serial number {request.SerialNumber} already exists");
        }

        var newDrone = await droneRepository.AddAsync(new Drone()
        {
            SerialNumber = request.SerialNumber,
            Model = (DroneModel)Enum.Parse(typeof(DroneModel), request.Model, true),
            BatteryCapacity = request.BatteryCapacity,
            State = DroneState.IDLE,
            WeightLimit = request.WeightLimit
        });
        
        var dto = _mapper.Map<DroneDto>(newDrone);
        response.Drone = dto;
        return Results.Created($"api/drones/{dto.Id}", response);
    }
}
