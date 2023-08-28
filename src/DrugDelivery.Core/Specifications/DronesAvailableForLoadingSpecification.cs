using Ardalis.Specification;
using DrugDelivery.Core.Entities;
using System.Linq;

namespace DrugDelivery.Core.Specifications
{
    public class DronesAvailableForLoadingSpecification : Specification<Drone>
    {
        public DronesAvailableForLoadingSpecification()
        {
            Query.Where(item => item.State == DroneState.IDLE);
        }
    }
}
