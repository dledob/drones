using System;
using System.Threading.Tasks;

namespace DrugDelivery.Core.Interfaces
{
    public interface IAuditLog
    {
        Task CreateLogAsync(string action, string tableName, string value, DateTime? date = null, Guid? userId = null);
    }
}
