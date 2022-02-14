using MediatR;
using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoCarro.Test;

public class ProprietarioService : IProprietarioService
{
    private readonly IProprietarioRepository _proprietarioRepository;

    public ProprietarioService(IProprietarioRepository ProprietarioRepository)
    {
        _proprietarioRepository = ProprietarioRepository;
    }

    public async Task<IEnumerable<Proprietario>> ObterTodos()
    {
        return await _proprietarioRepository.ObterTodos();
    }

    public async Task<bool> Adicionar(Proprietario Proprietario)
    {
        if (!Proprietario.EhValido())
            return false;

        return await _proprietarioRepository.Adicionar(Proprietario);

    }

    public async Task<bool> Atualizar(Proprietario Proprietario)
    {
        if (!Proprietario.EhValido())
            return false;

        return await _proprietarioRepository.Atualizar(Proprietario);

    }
    public void Dispose()
    {
        _proprietarioRepository?.Dispose();
    }
}
