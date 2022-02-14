using FluentValidation;
using SoCarro.Core.Messages;
using SoCarro.WebApi.ViewModels;

namespace SoCarro.WebApi.Application.Commands;

public class VeiculoEmailCommand : Command
{
    public EmailVeiculoViewModel VeiculoEmail { get; set; }

    public VeiculoEmailCommand(Guid id, EmailVeiculoViewModel veiculoEmail)
    {
        AggregateId = id;
        VeiculoEmail = veiculoEmail;
    }

    public override bool EhValido()
    {
        ValidationResult = new EnviarEmailVeiculoValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class EnviarEmailVeiculoValidation : AbstractValidator<VeiculoEmailCommand>
    {
        public EnviarEmailVeiculoValidation()
        {
            RuleFor(c => c.VeiculoEmail.Veiculo.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do Veiculo inválido");

            RuleFor(c => c.VeiculoEmail.Origem)
                .NotEmpty()
                .WithMessage("O email de origem não foi informado");

            RuleFor(c => c.VeiculoEmail.Destino)
                .NotEmpty()
                .WithMessage("O email de destino informado não é válido.");

            RuleFor(c => c.VeiculoEmail.Assunto)
                .NotEmpty()
                .WithMessage("O assunto informado não é válido.");

            RuleFor(c => c.VeiculoEmail.Mensagem)
                .NotEmpty()
                .WithMessage("A mensagem informado não é válido.");
        }
    }
}