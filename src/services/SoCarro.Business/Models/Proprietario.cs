using SoCarro.Business.Validations;
using SoCarro.Core.DomainObjects;

namespace SoCarro.Business.Models;

public class Proprietario : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string  Documento { get; private set; }
    public TipoStatus Status { get; private set; }
    public TipoProprietario TipoProprietario { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public Endereco Endereco { get; private set; }
    public Email Email { get; private set; }

    // EF Relation
    public Veiculo Veiculo { get; protected set; }

    protected Proprietario() { }
    public Proprietario(string nome, string documento, TipoStatus status, TipoProprietario tipoProprietario,
              Endereco endereco, string email)
    {
        Nome = nome;
        Documento = documento.Replace(".", "").Replace("-", ""); ;
        Status = status;
        TipoProprietario = tipoProprietario;
        Endereco = endereco;
        Email = new Email(email);
        Validar();
    }
    public void Ativar() => Status = TipoStatus.Ativo;

    public void Cancelar() => Status = TipoStatus.Cancelado;

    public void TrocarEmail(string email) => Email = new Email(email);

    public void AtribuirEndereco(Endereco endereco) => Endereco = endereco;

    public bool Validar()
    {
        try
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do proprietario não pode estar vazio");
            Validacoes.ValidarSeVazio(Documento, "O campo Documento não pode estar vazio");
            Validacoes.ValidarSeVazio(Email.Endereco, "O campo Email não pode estar vazio");
            return true;
        }
        catch (DomainException)
        {

            return false;
        }
    }

    public override bool EhValido()
    {
        return new ProprietarioValidation().Validate(this).IsValid;
    }
}

