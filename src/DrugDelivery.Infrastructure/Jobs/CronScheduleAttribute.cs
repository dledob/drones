
namespace DrugDelivery.Infrastructure.Jobs
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CronScheduleAttribute : Attribute
    {
        public string CronSchedule { get; }

        public CronScheduleAttribute(string name)
        {
            CronSchedule = name;
        }
    }
}
