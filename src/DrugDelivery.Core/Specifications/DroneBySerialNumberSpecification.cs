using Ardalis.Specification;
using DrugDelivery.Core.Entities;
using System.Linq;

namespace DrugDelivery.Core.Specifications
{
    public class DroneBySerialNumberSpecification : Specification<Drone>
    {
        public DroneBySerialNumberSpecification(string serial)
        {
            Query.Where(item => serial == item.SerialNumber);
        }
    }
}
