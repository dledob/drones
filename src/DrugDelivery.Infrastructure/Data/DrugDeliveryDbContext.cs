using System.Reflection;
using DrugDelivery.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrugDelivery.Infrastructure.Data;

public class DrugDeliveryDbContext : DbContext
{
    #pragma warning disable CS8618 // Required by Entity Framework
    public DrugDeliveryDbContext(DbContextOptions<DrugDeliveryDbContext> options) : base(options) {}

    public DbSet<Drone> Drones { get; set; }
    public DbSet<Medication> Medications { get; set; }
    public DbSet<LoadedMedication> LoadedMedications { get; set; }
    public DbSet<Core.Entities.AuditLog> AuditLogs { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
