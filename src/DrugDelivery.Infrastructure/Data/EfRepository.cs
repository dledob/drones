using Ardalis.Specification.EntityFrameworkCore;
using DrugDelivery.Core.Interfaces;

namespace DrugDelivery.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(DrugDeliveryDbContext dbContext) : base(dbContext)
    {
    }
}
