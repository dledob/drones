using DrugDelivery.HttpApi;
using DrugDelivery.HttpApi.DroneEndpoints;
using System;

namespace Drugdelivery.HttpApi.DroneEndpoints;

public class GetByIdDroneResponse : BaseResponse
{
    public GetByIdDroneResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdDroneResponse()
    {
    }

    public DroneDto Drone { get; set; }
}
