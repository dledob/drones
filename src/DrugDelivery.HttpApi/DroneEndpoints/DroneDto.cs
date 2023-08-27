using DrugDelivery.Core.Entities;
using System;

namespace DrugDelivery.HttpApi.DroneEndpoints
{
    public class DroneDto
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public DroneModel Model { get; set; }
        public decimal WeightLimit { get; set; }
        public decimal BatteryCapacity { get; set; }
        public DroneState State { get; set; }
    }
}
