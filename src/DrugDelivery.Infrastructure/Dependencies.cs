using DrugDelivery.Infrastructure.Data;
using DrugDelivery.Infrastructure.Identity;
using DrugDelivery.Infrastructure.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.AspNetCore;
using System.Reflection;

namespace DrugDelivery.Infrastructure;

public static class Dependencies
{
    public static void ConfigureDatabase(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<DrugDeliveryDbContext>(c =>
               c.UseInMemoryDatabase("DrugDelivery"));

        services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseInMemoryDatabase("Identity"));
    }

    public static void ConfigureQuartz(Assembly assembly, IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            var jobs = assembly.GetExportedTypes()
            .Where(x =>
                x.IsPublic &&
                x.IsClass &&
                !x.IsAbstract &&
                typeof(IJob).IsAssignableFrom(x))
            .ToArray();
            // base Quartz scheduler, job and trigger configuration
            foreach (var job in jobs)
            {
                AddJobAndTrigger(q, job);
            }
        });

        // ASP.NET Core hosting
        services.AddQuartzServer(options =>
        {
            // when shutting down we want jobs to complete gracefully
            options.WaitForJobsToComplete = true;
        });
    }

    private static void AddJobAndTrigger(
        this IServiceCollectionQuartzConfigurator quartz, Type type)
    {
        // Use the name of the IJob as the appsettings.json key
        string jobName = type.Name;

        // Try and load the schedule from configuration
        var configKey = $"Quartz:{jobName}";
        var cronAttribute = type.GetCustomAttributes(typeof(CronScheduleAttribute), true).FirstOrDefault() as CronScheduleAttribute;
        if( cronAttribute == null)
        {
            return;
        }
        var cronSchedule = cronAttribute.CronSchedule;

        // Some minor validation
        if (string.IsNullOrEmpty(cronSchedule))
        {
            throw new Exception($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");
        }

        // register the job as before
        var jobKey = new JobKey(jobName);
        quartz.AddJob(type, jobKey);

        quartz.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity(jobName + "-trigger")
            .WithCronSchedule(cronSchedule)); // use the schedule from configuration
    }
}
