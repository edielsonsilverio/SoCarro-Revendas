using SoCarro.Business.Models;
using SoCarro.Core.Data;
using System.Linq.Expressions;

namespace SoCarro.Business.Intefaces;

public interface IMarcaRepository : IRepository<Marca>
{
    Task<IEnumerable<Marca>> ObterTodos(Expression<Func<Marca, bool>> filter = null);
    Task<Marca> ObterPorId(Guid modelId);
    Task<bool> Adicionar(Marca model);
    Task<bool> Atualizar(Marca model);
}
