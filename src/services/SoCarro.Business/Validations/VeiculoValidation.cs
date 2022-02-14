using FluentValidation;
using SoCarro.Business.Models;
using SoCarro.Business.Validations.Documentos;

namespace SoCarro.Business.Validations;

public class VeiculoValidation : AbstractValidator<Veiculo>
{
    public VeiculoValidation()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.Renavam.Length).Equal(RenavamValidacao.TamanhoRenavam)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

        RuleFor(x => x.MarcaId)
            .NotEqual(Guid.Empty).WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(x => x.ProprietarioId)
            .NotEqual(Guid.Empty).WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(x => x.Quilometragem)
            .GreaterThan(-1).WithMessage("O campo {PropertyName} precicsa ser maior ou igual que zero");

        RuleFor(x => x.Valor)
            .GreaterThan(-1).WithMessage("O campo {PropertyName} precicsa ser maior ou igual que zero");

     }
}
