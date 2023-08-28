using DrugDelivery.HttpApi;
using System;

namespace Drugdelivery.HttpApi.DroneEndpoints;

public class DroneBatteryLevelEndpointRequest : BaseRequest
{
    public Guid DroneId { get; init; }

    public DroneBatteryLevelEndpointRequest(Guid id)
    {
        DroneId = id;
    }
}
