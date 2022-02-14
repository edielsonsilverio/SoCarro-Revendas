using SoCarro.Business.Models;

namespace SoCarro.Business.Intefaces;

public interface IProprietarioService : IDisposable
{
    Task<bool> Adicionar(Proprietario model);
    Task<bool> Atualizar(Proprietario model);
}
