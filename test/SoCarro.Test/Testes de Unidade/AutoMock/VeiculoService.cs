using MediatR;
using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using SoCarro.Core.Mediator;
using SoCarro.Core.Messages.Integration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoCarro.Test;

public class VeiculoService : IVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IMediatorHandler _mediator;

    public VeiculoService(IVeiculoRepository VeiculoRepository, IMediatorHandler mediator)
    {
        _veiculoRepository = VeiculoRepository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<Veiculo>> ObterTodos()
    {
        return await _veiculoRepository.ObterTodos();
    }

    public async Task<bool> Adicionar(Veiculo Veiculo)
    {
        if (!Veiculo.EhValido())
            return false;

        await _veiculoRepository.Adicionar(Veiculo);

        await _mediator.PublicarEvento(new EnviarEmailIntegrationEvent("admin@me.com", "admin@me.com", "Novo Registro", "Dê uma olhada!"));
       
        return true;
    }

    public async Task<bool> Atualizar(Veiculo Veiculo)
    {
        if (!Veiculo.EhValido())
            return false;

        await _veiculoRepository.Atualizar(Veiculo);

        return true;

    }
    public void Dispose()
    {
        _veiculoRepository.Dispose();
    }
}