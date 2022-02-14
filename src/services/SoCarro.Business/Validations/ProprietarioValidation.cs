using FluentValidation;
using SoCarro.Business.Models;
using SoCarro.Business.Validations.Documentos;

namespace SoCarro.Business.Validations;

public class ProprietarioValidation : AbstractValidator<Proprietario>
{
    public ProprietarioValidation()
    {
        RuleFor(f => f.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100)
            .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        When(f => f.TipoProprietario == TipoProprietario.PessoaFisica, () =>
        {
            RuleFor(f => f.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
        });

        When(f => f.TipoProprietario == TipoProprietario.PessoaJuridica, () =>
        {
            RuleFor(f => f.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            RuleFor(f => CnpjValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
        });

        
        RuleFor(c => c.Endereco.Logradouro)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Endereco.Bairro)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Endereco.Cep)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(8).WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres");

        RuleFor(c => c.Endereco.Cidade)
            .NotEmpty().WithMessage("A campo {PropertyName} precisa ser fornecida")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Endereco.Estado)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Endereco.Numero)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(1, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}
