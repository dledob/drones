
using DrugDelivery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugDelivery.Infrastructure.Data.Config
{
    public class DroneConfiguration : IEntityTypeConfiguration<Drone>
    {
        public void Configure(EntityTypeBuilder<Drone> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.SerialNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Model)
                .HasConversion<string>();

            builder.Property(d => d.State)
                .HasConversion<string>();
        }
    }
}
