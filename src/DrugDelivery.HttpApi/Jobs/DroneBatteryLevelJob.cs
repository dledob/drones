using DrugDelivery.Core.Entities;
using DrugDelivery.Core.Interfaces;
using DrugDelivery.Core.Specifications;
using DrugDelivery.Infrastructure.Jobs;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace DrugDelivery.HttpApi.Jobs
{
    [CronSchedule("0/30 * * * * ?")]
    public class DroneBatteryLevelJob : IJob
    {
        private readonly ILogger<DroneBatteryLevelJob> _logger;
        private readonly IReadRepository<Drone> _droneRepository;
        private readonly IAuditLog _auditLog;
        public DroneBatteryLevelJob(
            ILogger<DroneBatteryLevelJob> logger,
            IReadRepository<Drone> droneRepository,
            IAuditLog auditLog
            ) 
        {
            _logger = logger;
            _droneRepository = droneRepository;
            _auditLog = auditLog;
        }       

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Start {nameof(DroneBatteryLevelJob)}");
            var spec = new DroneWithBatteryLevelRangeSpecification();
            var drones = await _droneRepository.ListAsync(spec);
            foreach (var drone in drones )
            {
                //TODO Set Action Types
                await _auditLog.CreateLogAsync("Check Battery Level Below 25%", nameof(Drone), drone.Id.ToString(), DateTime.UtcNow);
            }
            var message = $"{drones.Count} Drone(s) with Battery Level Below 25%";
            if (drones.Count == 0) 
            {
                message = $"No Drone with Battery Level Below 25%";
            }
            _logger.LogInformation(message);
        }
    }
}
