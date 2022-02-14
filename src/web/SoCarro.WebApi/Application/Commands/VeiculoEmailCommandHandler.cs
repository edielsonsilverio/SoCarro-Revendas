using FluentValidation.Results;
using MediatR;
using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using SoCarro.Core.Messages;
using SoCarro.WebApi.Application.Events;

namespace SoCarro.WebApi.Application.Commands;

public class VeiculoEmailCommandHandler : CommandHandler,
                                         IRequestHandler<VeiculoEmailCommand, ValidationResult>
{
    private readonly IVeiculoRepository _veiculoRepository;

    public VeiculoEmailCommandHandler(IVeiculoRepository veiculoRepository)
    {
        _veiculoRepository = veiculoRepository;
    }

    public async Task<ValidationResult> Handle(VeiculoEmailCommand message, CancellationToken cancellationToken)
    {
        //Verifica se existe erro.
        if (!message.EhValido()) return message.ValidationResult;

        //Cria um novo veiculo.
        var veiculo = new Veiculo(message.VeiculoEmail.Veiculo.Nome, 
             message.VeiculoEmail.Veiculo.Renavam, message.VeiculoEmail.Veiculo.Quilometragem, message.VeiculoEmail.Veiculo.Valor,
             message.VeiculoEmail.Veiculo.MarcaId, message.VeiculoEmail.Veiculo.ProprietarioId);

        //Verifica se já existe um veiculo com o número de cpf
        var veiculoExistente = await _veiculoRepository.ObterTodos(x=> x.Renavam == veiculo.Renavam);
 
        if (veiculoExistente != null)
        {
            AdicionarErro("Este Renavam já está em uso.");
            return ValidationResult;
        }

        //Adiciona o novo veiculo
        await _veiculoRepository.Adicionar(veiculo);

        //Envio o evento caso sucesso
        veiculo.AdicionarEvento(new VeiculoEmailEvent(veiculo.Id, message.VeiculoEmail));

        //Salva as alterações e retorna.
        return await PersistirDados(_veiculoRepository.UnitOfWork);
    }
}