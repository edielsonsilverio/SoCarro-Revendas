using FluentValidation;
using SoCarro.Business.Models;

namespace SoCarro.Business.Validations;

public class ModeloValidation : AbstractValidator<Modelo>
{
    public ModeloValidation()
    {
        RuleFor(m => m.Descricao)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(m => m.AnoFabricacao)
            .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("O campo {PropertyName} precicsa ser menor que " + (DateTime.Now.Year))
            .GreaterThan(1950).WithMessage("O campo {PropertyName} precicsa ser maior ou igual que zero");

        RuleFor(m => m.AnoModelo)
            .LessThan(DateTime.Now.Year + 2).WithMessage("O campo {PropertyName} precicsa ser menor que " + (DateTime.Now.Year + 2))
            .GreaterThan(1950).WithMessage("O campo {PropertyName} precicsa ser maior ou igual que zero");
    }
}