using SoCarro.Core.DomainObjects;

namespace SoCarro.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<string> PersistirDados();
}
