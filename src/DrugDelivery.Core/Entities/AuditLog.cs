using DrugDelivery.Core.Interfaces;
using System;

namespace DrugDelivery.Core.Entities
{
    public class AuditLog : BaseEntity, IAggregateRoot
    {       
        public string Action { get; set; }
        public string TableName { get; set; }
        public string Value { get; set; }
        public DateTime? Date { get; set; }
        public Guid? UserId { get; set; }

        #pragma warning disable CS8618 // Required by Entity Framework
        public AuditLog() : base() { }
        public AuditLog(Guid id) : base(id) { }
    }
}
