using DrugDelivery.Core.Entities;
using DrugDelivery.HttpApi;
using DrugDelivery.HttpApi.DroneEndpoints;
using System.Collections.Generic;

namespace Drugdelivery.HttpApi.DroneEndpoints;

public class LoadingDroneRequest : BaseRequest
{
    public List<MedicationDto> Medications { get; set; }
}
