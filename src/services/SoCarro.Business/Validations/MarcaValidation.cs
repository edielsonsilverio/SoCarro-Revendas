using FluentValidation;
using SoCarro.Business.Models;

namespace SoCarro.Business.Validations;

public class MarcaValidation : AbstractValidator<Marca>
{
    public MarcaValidation()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}
