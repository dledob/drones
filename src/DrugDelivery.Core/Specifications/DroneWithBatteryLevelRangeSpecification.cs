using Ardalis.Specification;
using DrugDelivery.Core.Entities;
using System;
using System.Linq;

namespace DrugDelivery.Core.Specifications
{
    public class DroneWithBatteryLevelRangeSpecification : Specification<Drone>
    {
        public DroneWithBatteryLevelRangeSpecification(decimal min = 0, decimal max = 25)
        {
            Query.Where(item => item.BatteryCapacity >= min && item.BatteryCapacity < max);
        }
    }
}
