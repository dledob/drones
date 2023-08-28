using DrugDelivery.HttpApi;
using DrugDelivery.HttpApi.DroneEndpoints;
using System;

namespace Drugdelivery.HttpApi.DroneEndpoints;

public class LoadingDroneResponse : BaseResponse
{
    public LoadingDroneResponse(Guid correlationId) : base(correlationId)
    {
    }

    public LoadingDroneResponse()
    {
    }
}
