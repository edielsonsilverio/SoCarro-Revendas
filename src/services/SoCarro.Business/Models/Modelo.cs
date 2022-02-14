using SoCarro.Core.DomainObjects;

namespace SoCarro.Business.Models;

public class Modelo : Entity
{
    public string Descricao { get; private set; }
    public int AnoFabricacao { get; private set; }
    public int AnoModelo { get; private set; }

    public Guid VeiculoId { get; private set; }

    // EF Relation
    public Veiculo Veiculo { get; protected set; }

    public Modelo(string descricao, int anoFabricacao, int anoModelo, Guid veiculoId)
    {
        Descricao = descricao;
        AnoFabricacao = anoFabricacao;
        AnoModelo = anoModelo;
        VeiculoId = veiculoId;
    }

    // EF Constructor
    protected Modelo() { }
}