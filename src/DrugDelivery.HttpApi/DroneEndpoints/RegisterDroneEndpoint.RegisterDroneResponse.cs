using DrugDelivery.HttpApi;
using DrugDelivery.HttpApi.DroneEndpoints;
using System;

namespace Drugdelivery.HttpApi.DroneEndpoints;

public class RegisterDroneResponse : BaseResponse
{
    public RegisterDroneResponse(Guid correlationId) : base(correlationId)
    {
    }

    public RegisterDroneResponse()
    {
    }

    public DroneDto Drone { get; set; }
}
