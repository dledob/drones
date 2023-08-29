using DrugDelivery.Core.Entities;
using DrugDelivery.HttpApi;

namespace Drugdelivery.HttpApi.DroneEndpoints;

public class RegisterDroneRequest : BaseRequest
{
    public string SerialNumber { get; set; }
    public string Model { get; set; }
    public decimal WeightLimit { get; set; }
    public decimal BatteryCapacity { get; set; }
}
