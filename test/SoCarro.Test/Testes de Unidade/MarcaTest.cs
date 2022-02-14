using SoCarro.Business.Models;
using Xunit;

namespace SoCarro.Test;

public class MarcaTest
{
    [Fact(DisplayName = "Nome não ser nulo ou vário")]
    [Trait("Unitário", "Model Marca")]
    public void Marca_Nome_NaoDeveSerNuloOuVazio()
    {
        // Arrange
        var model = new Marca("Teste");

        // Assert
        Assert.False(string.IsNullOrEmpty(model.Nome));
    }

    [Fact(DisplayName = "Nome não deve ser igual")]
    [Trait("Unitário", "Model Marca")]
    public void Marca_Nome_NaoDeveIgual()
    {
        // Arrange
        var model = new Marca("Teste");

        // Act
        var nome = "Teste";

        // Assert
        Assert.Equal(nome, model.Nome);
    }

    [Fact(DisplayName = "O Status deve ser cancelado")]
    [Trait("Unitário", "Model Marca")]
    public void Marca_Status_DeveSerCancelado()
    {
        // Arrange
        var model = new Marca("Teste");

        // Act
        model.Cancelar();

        // Assert
        Assert.Equal(TipoStatus.Cancelado, model.Status);
    }

    [Fact(DisplayName = "O Status não deve ser cancelado")]
    [Trait("Unitário", "Model Marca")]
    public void Marca_Status_NaoDeveSerCancelado()
    {
        // Arrange
        var model = new Marca("Teste");

        // Act
        model.Ativar();

        // Assert
        Assert.NotEqual(TipoStatus.Cancelado, model.Status);
    }

    [Fact(DisplayName = "Validar as informações")]
    [Trait("Unitário", "Model Marca")]
    public void Marca_Campo_DevemEstarValidado()
    {
        // Arrange
        var model = new Marca("Teste");

        // Act
        var resultado = model.EhValido();

        // Assert
        Assert.True(resultado);
    }

    [Fact(DisplayName = "Validar as informações")]
    [Trait("Unitário", "Model Marca")]
    public void Marca_Campo_NaoDevemEstarValidado()
    {
        // Arrange
        var model = new Marca("");

        // Act
        var resultado = model.EhValido();

        // Assert
        Assert.False(resultado);
    }
}
