
using DrugDelivery.Core.Interfaces;
using System;

namespace DrugDelivery.Core.Entities
{
    public class Medication : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Weight { get; set; }
        public string Image { get; set; }

        #pragma warning disable CS8618 // Required by Entity Framework
        public Medication() : base()
        {
        }
        public Medication(Guid id) : base(id)
        {
        }
    }
}
