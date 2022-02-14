using SoCarro.Business.Models;

namespace SoCarro.Business.Intefaces;

public interface IVeiculoService : IDisposable
{
    Task<bool> Adicionar(Veiculo model);
    Task<bool> Atualizar(Veiculo model);
}