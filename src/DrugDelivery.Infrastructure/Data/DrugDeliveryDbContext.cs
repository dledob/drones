using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DrugDelivery.Infrastructure.Data;

public class DrugDeliveryDbContext : DbContext
{
    #pragma warning disable CS8618 // Required by Entity Framework
    public DrugDeliveryDbContext(DbContextOptions<DrugDeliveryDbContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
