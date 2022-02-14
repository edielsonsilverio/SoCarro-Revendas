using SoCarro.Business.Validations;
using SoCarro.Core.DomainObjects;

namespace SoCarro.Business.Models;

public class Veiculo : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Renavam { get; private set; }
    public int Quilometragem { get; private set; }
    public decimal Valor { get; private set; }
    public TipoStatusVeiculo Status { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public Modelo Modelo { get; private set; }

    // EF Relation
    public Guid MarcaId { get; private set; }
    public Marca Marca { get; protected set; }

    public Guid ProprietarioId { get; private set; }
    public Proprietario Proprietario { get; protected set; }


    // EF Relation
    protected Veiculo() { }

    public Veiculo(string nome, string renavam, int quilometragem, decimal valor, Guid marcaId, Guid proprietarioId)
    {
        Nome = nome;
        Renavam = renavam;
        Quilometragem = quilometragem;
        Valor = valor;
        MarcaId = marcaId;
        ProprietarioId = proprietarioId;

        //Validar();
    }

    public void EstaDisponivel() => Status = TipoStatusVeiculo.Disponivel;

    public void EstaIndisponivel() => Status = TipoStatusVeiculo.Indisponivel;

    public void EstaVendido() => Status = TipoStatusVeiculo.Vendido;

    public void AtribuirModelo(Marca marca) => Marca = marca;

    public void AtribuirProprietario(Proprietario proprietario) => Proprietario = proprietario;

    public bool Validar()
    {
        try
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do proprietario não pode estar vazio");
            Validacoes.ValidarSeVazio(Renavam, "O campo Renavam não pode estar vazio");
            //Validacoes.ValidarSeIgual(ProprietarioId, Guid.Empty, "O campo ProprietarioId do proprietario não pode estar vazio");
            //Validacoes.ValidarSeIgual(MarcaId, Guid.Empty, "O campo MarcaId da Marca não pode estar vazio");
            Validacoes.ValidarSeMenorQue(Valor, 1, "O campo Valor do veículo não pode se menor igual a 0");
            Validacoes.ValidarSeMenorQue(Quilometragem, 1, "O campo Quilometragem do veículo não pode se menor igual a 0");
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public override bool EhValido()
    {
        return new VeiculoValidation().Validate(this).IsValid;
    }
}
