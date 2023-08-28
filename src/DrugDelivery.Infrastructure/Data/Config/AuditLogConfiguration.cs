
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugDelivery.Infrastructure.Data.Config
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<Core.Entities.AuditLog>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.AuditLog> builder)
        {
            builder.HasKey(d => d.Id);
        }
    }
}
