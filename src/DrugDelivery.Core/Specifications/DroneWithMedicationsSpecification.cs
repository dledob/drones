using Ardalis.Specification;
using DrugDelivery.Core.Entities;
using System;
using System.Linq;

namespace DrugDelivery.Core.Specifications
{
    public class DroneWithMedicationsSpecification : Specification<Drone>
    {
        public DroneWithMedicationsSpecification(Guid id)
        {
            Query.Where(item => id == item.Id)
                .Include(item => item.LoadedMedications)
                .ThenInclude(e => e.Medication);
        }
    }
}
