namespace SoCarro.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}