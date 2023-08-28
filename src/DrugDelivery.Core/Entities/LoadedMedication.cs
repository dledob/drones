
using DrugDelivery.Core.Interfaces;
using System;

namespace DrugDelivery.Core.Entities
{
    public class LoadedMedication : BaseEntity, IAggregateRoot
    {
        public Guid DroneId { get; set; }
        public Drone Drone { get; set; }
        public Guid MedicationId { get; set; }
        public Medication Medication { get; set; }

#pragma warning disable CS8618 // Required by Entity Framework
        public LoadedMedication() : base()
        {
        }
        public LoadedMedication(Guid id) : base(id)
        {
        }
    }
}
