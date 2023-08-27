
using DrugDelivery.Core.Interfaces;
using System;

namespace DrugDelivery.Core.Entities
{
    public class Drone : BaseEntity, IAggregateRoot
    {
        public string SerialNumber { get; set; }
        public DroneModel Model { get; set; }
        public decimal WeightLimit { get; set; }
        public decimal BatteryCapacity { get; set; }
        public DroneState State { get; set; }

        #pragma warning disable CS8618 // Required by Entity Framework
        public Drone()
        {
        }
        public Drone(Guid id) : base(id)
        {
        }
    }

    public enum DroneModel
    {
        LIGHTWEIGHT, MIDDLEWEIGHT, CRUISERWEIGHT, HEAVYWEIGHT
    }

    public enum DroneState
    {
        IDLE, LOADING, LOADED, DELIVERING, DELIVERED, RETURNING
    }
}
