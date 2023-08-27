using DrugDelivery.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrugDelivery.Infrastructure;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        /*services.AddDbContext<CatalogContext>(c =>
               c.UseInMemoryDatabase("Catalog"));*/

        services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseInMemoryDatabase("Identity"));
    }
}
