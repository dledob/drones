using DrugDelivery.Core.Interfaces;

namespace DrugDelivery.Infrastructure
{
    public class AuditLog : IAuditLog
    {
        private readonly IRepository<Core.Entities.AuditLog> _repository;
        public AuditLog(IRepository<Core.Entities.AuditLog> repository) 
        { 
            _repository = repository;
        }
        public async Task CreateLogAsync(string action, string tableName, string value, DateTime? date = null, Guid? userId = null)
        {
            await _repository.AddAsync(new Core.Entities.AuditLog
            {
                Action = action,
                TableName = tableName,
                Value = value,
                Date = date,
                UserId = userId
            });
        }
    }
}
