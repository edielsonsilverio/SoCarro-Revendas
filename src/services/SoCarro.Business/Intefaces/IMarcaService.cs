using SoCarro.Business.Models;

namespace SoCarro.Business.Intefaces;

public interface IMarcaService : IDisposable
{
    Task<bool> Adicionar(Marca model);
    Task<bool> Atualizar(Marca model);
}
