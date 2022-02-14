using SoCarro.Business.Models;
using Xunit;

namespace SoCarro.Test;

public class VeiculoTest
{
    [Fact(DisplayName = "Os Campos devem ser Obrigatórios")]
    [Trait("Unitário", "Model Veiculo")]
    public void Veiculo_Validacao_CamposDevemSerObrigatorio()
    {
        // Arrange
        var modelModelo =  new Modelo("Trend",2022,2022,System.Guid.Empty);
        var model = new Veiculo("Teste", "83663783642", 100,152500,System.Guid.Empty,System.Guid.Empty);

        // Act & Assert
        var result = model.Validar();

        // Assert 
        Assert.True(result);
    }

    [Fact(DisplayName = "Nome não deve ser igual")]
    [Trait("Unitário", "Model Veiculo")]
    public void Veiculo_Nome_NaoDeveIgual()
    {
        // Arrange
        var modelModelo = new Modelo("Trend", 2022, 2022, System.Guid.Empty);
        var model = new Veiculo("Teste", "83663783642", 100, 152500, System.Guid.Empty, System.Guid.Empty);

        // Act
        var nome = "Teste";

        // Assert
        Assert.Equal(nome, model.Nome);
    }

    [Fact(DisplayName = "O Status deve estar disponível")]
    [Trait("Unitário", "Model Veiculo")]
    public void Veiculo_Status_DeveEstarDisponivel()
    {
        // Arrange
        var modelModelo = new Modelo("Trend", 2022, 2022, System.Guid.Empty);
        var model = new Veiculo("Teste", "83663783642", 100, 152500, System.Guid.Empty, System.Guid.Empty);
       
        // Act
        model.EstaDisponivel();

        // Assert
        Assert.Equal(TipoStatusVeiculo.Disponivel, model.Status);
    }

    [Fact(DisplayName = "O Status depois de vendido não deve mudar o status")]
    [Trait("Unitário", "Model Veiculo")]
    public void Veiculo_Status_DepoisDeVendidoNaoPodeEstarDisponivelOuCancelado()
    {
        // Arrange
        var modelModelo = new Modelo("Trend", 2022, 2022, System.Guid.Empty);
        var model = new Veiculo("Teste", "83663783642", 100, 152500, System.Guid.Empty, System.Guid.Empty);

        // Act
        model.EstaVendido();

        // Assert
        Assert.NotEqual(TipoStatusVeiculo.Disponivel, model.Status);
    }
}