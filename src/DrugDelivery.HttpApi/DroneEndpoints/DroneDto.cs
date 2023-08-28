using DrugDelivery.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace DrugDelivery.HttpApi.DroneEndpoints
{
    public class DroneDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public DroneModel Model { get; set; }
        [Required]
        public decimal WeightLimit { get; set; }
        [Required]
        public decimal BatteryCapacity { get; set; }
        public DroneState State { get; set; }
    }
}
