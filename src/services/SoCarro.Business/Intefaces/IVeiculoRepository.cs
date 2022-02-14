using SoCarro.Business.Models;
using SoCarro.Core.Data;
using System.Linq.Expressions;

namespace SoCarro.Business.Intefaces;

public interface IVeiculoRepository : IRepository<Veiculo>
{
    Task<IEnumerable<Veiculo>> ObterTodos(Expression<Func<Veiculo, bool>> filter = null);
    Task<Veiculo> ObterPorId(Guid modelId);
    Task<bool> Adicionar(Veiculo model);
    Task<bool> Atualizar(Veiculo model);
    Task<bool> VerificarStatus(Guid modelId,TipoStatusVeiculo status);
    Task<bool> AtualizarMarca(Marca model);
    Task<bool> AtualizarProprietario(Proprietario model);
    Task<bool> AtualizarModelo(Modelo model);
    Task<Modelo> ObterModeloPorId(Guid id);
    Task<Proprietario> ObterProprietarioPorId(Guid id);
}
