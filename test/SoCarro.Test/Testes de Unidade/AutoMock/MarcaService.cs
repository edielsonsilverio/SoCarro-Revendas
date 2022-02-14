using MediatR;
using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoCarro.Test;

public class MarcaService : IMarcaService
{
    private readonly IMarcaRepository _marcaRepository;

    public MarcaService(IMarcaRepository MarcaRepository)
    {
        _marcaRepository = MarcaRepository;
    }

    public async Task<IEnumerable<Marca>> ObterTodos()
    {
        return await _marcaRepository.ObterTodos();
    }

    public async Task<bool> Adicionar(Marca Marca)
    {
        if (!Marca.EhValido())
            return false;

        return await _marcaRepository.Adicionar(Marca);

    }

    public async Task<bool> Atualizar(Marca Marca)
    {
        if (!Marca.EhValido())
            return false;

        return await _marcaRepository.Atualizar(Marca);

    }
    public void Dispose()
    {
        _marcaRepository.Dispose();
    }
}
