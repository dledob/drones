using Ardalis.Specification;

namespace DrugDelivery.Core.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
