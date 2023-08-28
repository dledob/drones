using DrugDelivery.HttpApi;
using DrugDelivery.HttpApi.DroneEndpoints;
using System;
using System.Collections.Generic;

namespace Drugdelivery.HttpApi.DroneEndpoints;

public class LoadedMedicationsResponse : BaseResponse
{
    public LoadedMedicationsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public LoadedMedicationsResponse()
    {
    }

    public ICollection<MedicationDto> Medications { get; set; }
}
