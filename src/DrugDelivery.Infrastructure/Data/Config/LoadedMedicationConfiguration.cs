
using DrugDelivery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugDelivery.Infrastructure.Data.Config
{
    public class LoadedMedicationConfiguration : IEntityTypeConfiguration<LoadedMedication>
    {
        public void Configure(EntityTypeBuilder<LoadedMedication> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasOne(e => e.Drone).WithMany(e => e.LoadedMedications).HasForeignKey(e => e.DroneId);
            builder.HasOne(e => e.Medication).WithMany().HasForeignKey(e => e.MedicationId);
        }
    }
}
