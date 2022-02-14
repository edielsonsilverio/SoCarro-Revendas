using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using SoCarro.Business.Validations;
using SoCarro.Core.DomainObjects;

namespace SoCarro.Business.Services;

public class VeiculoService : BaseService, IVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;

    public VeiculoService(IVeiculoRepository veiculoRepository,
                          INotificador notificador) : base(notificador)
    {
        _veiculoRepository = veiculoRepository;
    }

    public async Task<bool> Adicionar(Veiculo veiculo)
    {
        if (!ExecutarValidacao(new VeiculoValidation(), veiculo)) return false;

        if (!ExecutarValidacao(new VeiculoValidation(), veiculo)
            || !ExecutarValidacao(new ModeloValidation(), veiculo.Modelo)) return false;

        if (_veiculoRepository.ObterTodos(f => f.Renavam == veiculo.Renavam).Result.Any())
        {
            Notificar("Já existe um Renavam com este número.");
            return false;
        }
     
        await _veiculoRepository.Adicionar(veiculo);
        return true;
    }

    public async Task<bool> Atualizar(Veiculo veiculo)
    {
        if (!ExecutarValidacao(new VeiculoValidation(), veiculo)) return false;

        if (_veiculoRepository.ObterTodos(f => f.Nome != veiculo.Nome && f.Id == veiculo.Id).Result.Any())
        {
            Notificar("Não é possível alterar o nome.");
            return false;
        }

        if (_veiculoRepository.ObterTodos(f => f.Renavam == veiculo.Renavam && f.Id != veiculo.Id).Result.Any())
        {
            Notificar("Já existe um Renavam com este número.");
            return false;
        }

        if (_veiculoRepository.VerificarStatus(veiculo.Id, veiculo.Status).Result)
        {
            Notificar("Não é possível alterar o status para disponível.");
            return false;
        }

        await _veiculoRepository.Atualizar(veiculo);
        return true;
    }

    public async Task AtualizarModelo(Modelo modelo)
    {
        if (!ExecutarValidacao(new ModeloValidation(), modelo)) return;

        await _veiculoRepository.AtualizarModelo(modelo);
    }

    public void Dispose() => _veiculoRepository?.Dispose();
}
