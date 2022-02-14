using SoCarro.Business.Models;
using SoCarro.Core.Data;
using System.Linq.Expressions;

namespace SoCarro.Business.Intefaces;

public interface IProprietarioRepository : IRepository<Proprietario>
{
    Task<IEnumerable<Proprietario>> ObterTodos(Expression<Func<Proprietario, bool>> filter = null);
    Task<Proprietario> ObterPorId(Guid modelId);
    Task<bool> Adicionar(Proprietario model);
    Task<bool> Atualizar(Proprietario model);
    Task<Endereco> ObterEndereco(Guid id);
    Task<bool> AtualizarEndereco(Endereco model);
}
