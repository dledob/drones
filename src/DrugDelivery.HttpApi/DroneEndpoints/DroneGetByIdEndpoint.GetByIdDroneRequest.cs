using DrugDelivery.HttpApi;
using System;

namespace Drugdelivery.HttpApi.DroneEndpoints;

public class GetByIdDroneRequest : BaseRequest
{
    public Guid DroneId { get; init; }

    public GetByIdDroneRequest(Guid id)
    {
        DroneId = id;
    }
}
