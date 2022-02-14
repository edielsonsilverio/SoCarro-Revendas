using SoCarro.Business.Validations;
using SoCarro.Core.DomainObjects;

namespace SoCarro.Business.Models;

public  class Marca : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public TipoStatus Status { get; private set; }
    public DateTime DataCadastro { get; private set; }

    // EF Relation
    public Veiculo Veiculo { get; protected set; }
    protected Marca() { }

    public Marca(string nome)
    {
        Nome = nome;
        DataCadastro = DateTime.Now;
        Validar();
    }


    public void Ativar() => Status = TipoStatus.Ativo;

    public void Cancelar() => Status = TipoStatus.Cancelado;

    public bool Validar()
    {
        try
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome da marca não pode estar vazio");
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public override bool EhValido()
    {
        return new MarcaValidation().Validate(this).IsValid;
    }
}
