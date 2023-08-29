
using DrugDelivery.Core.Entities;
using Microsoft.Extensions.Logging;

namespace DrugDelivery.Infrastructure.Data;

public class DrugDeliveryDbContextSeed
{
    public static async Task SeedAsync(DrugDeliveryDbContext context,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            context.Drones.Add(new Drone("AB-34567", DroneModel.MIDDLEWEIGHT, 100M, 20M, DroneState.IDLE));
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;
            
            logger.LogError(ex.Message);
            await SeedAsync(context, logger, retryForAvailability);
            throw;
        }
    }
}
