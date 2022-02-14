using SoCarro.Business.Models;
using Xunit;

namespace SoCarro.Test;

public class ProprietarioTest
{
    [Fact(DisplayName = "Os Campos devem ser Obrigatórios")]
    [Trait("Unitário", "Model Proprietario")]
    public void Proprietario_Validacao_CamposDevemSerObrigatorio()
    {
        // Arrange
        var modelEndereco = new Endereco("Rua", "10", "58", "Centro", "78098000", "Cuiabá", "MT", System.Guid.Empty);
        var model = new Proprietario("Teste", "80292613067", TipoStatus.Ativo, TipoProprietario.PessoaJuridica, modelEndereco, "teste@hotmail.com");

        // Act & Assert
        var result = model.Validar();

        // Assert 
        Assert.True(result);
    }

    [Fact(DisplayName = "Nome não deve ser igual")]
    [Trait("Unitário", "Model Proprietario")]
    public void Proprietario_Nome_NaoDeveIgual()
    {
        // Arrange
        var modelEndereco = new Endereco("Rua", "10", "58", "Centro", "78098000", "Cuiabá", "MT", System.Guid.Empty);
        var model = new Proprietario("Teste", "80292613067", TipoStatus.Ativo, TipoProprietario.PessoaJuridica, modelEndereco, "teste@hotmail.com");

        // Act
        var nome = "Teste";

        // Assert
        Assert.Equal(nome, model.Nome);
    }


    [Fact(DisplayName = "Nome  deve ser igual")]
    [Trait("Unitário", "Model Proprietario")]
    public void Proprietario_Status_DeveSerCancelado()
    {
        // Arrange
        var modelEndereco = new Endereco("Rua", "10", "58", "Centro", "78098000", "Cuiabá", "MT", System.Guid.Empty);
        var model = new Proprietario("Teste", "80292613067", TipoStatus.Ativo, TipoProprietario.PessoaJuridica, modelEndereco, "teste@hotmail.com");

        // Act
        model.Cancelar();

        // Assert
        Assert.Equal(TipoStatus.Cancelado, model.Status);
    }

    [Fact(DisplayName = "O Status deve ser cancelado")]
    [Trait("Unitário", "Model Proprietario")]
    public void Proprietario_Status_NaoDeveSerCancelado()
    {
        // Arrange
        var modelEndereco = new Endereco("Rua", "10", "58", "Centro", "78098000", "Cuiabá", "MT", System.Guid.Empty);
        var model = new Proprietario("Teste", "80292613067", TipoStatus.Ativo, TipoProprietario.PessoaJuridica, modelEndereco, "teste@hotmail.com");

        // Act
        model.Ativar();

        // Assert
        Assert.NotEqual(TipoStatus.Cancelado, model.Status);
    }
}
