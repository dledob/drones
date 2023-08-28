using DrugDelivery.HttpApi;
using DrugDelivery.HttpApi.DroneEndpoints;
using System;

namespace Drugdelivery.HttpApi.DroneEndpoints;

public class DroneBatteryLevelEndpointResponse : BaseResponse
{
    public DroneBatteryLevelEndpointResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DroneBatteryLevelEndpointResponse()
    {
    }

    public decimal BatteryLevel { get; set; }
}
