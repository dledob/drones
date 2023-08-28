using DrugDelivery.HttpApi;
using DrugDelivery.HttpApi.DroneEndpoints;
using System;
using System.Collections.Generic;

namespace Drugdelivery.HttpApi.DroneEndpoints;

public class AvailableForLoadingResponse : BaseResponse
{
    public AvailableForLoadingResponse(Guid correlationId) : base(correlationId)
    {
    }

    public AvailableForLoadingResponse()
    {
    }

    public List<DroneDto> Drones { get; set; }
}
