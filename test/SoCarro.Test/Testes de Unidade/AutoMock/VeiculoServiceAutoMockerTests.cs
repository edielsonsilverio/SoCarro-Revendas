using Moq;
using Moq.AutoMock;
using SoCarro.Business.Intefaces;
using System.Threading.Tasks;
using Xunit;

namespace SoCarro.Test;

[Collection(nameof(VeiculoAutoMockerCollection))]
public class VeiculoServiceAutoMockerTests
{
    readonly VeiculoTestsAutoMockerFixture _veiculoTestsBogus;

    public VeiculoServiceAutoMockerTests(VeiculoTestsAutoMockerFixture veiculoTestsFixture)
    {
        _veiculoTestsBogus = veiculoTestsFixture;

    }

    [Fact(DisplayName = "Adicionar Veiculo com Sucesso")]
    [Trait("Mock", "Veiculo Service AutoMock Tests")]
    public async Task VeiculoService_Adicionar_DeveExecutarComSucesso()
    {
        // Arrange
        var veiculo = _veiculoTestsBogus.GerarVeiculoValido();
        var mocker = new AutoMocker();
        var veiculoService = mocker.CreateInstance<VeiculoService>();

        // Act
        await veiculoService.Adicionar(veiculo);

        // Assert
        Assert.True(veiculo.Validar());
        mocker.GetMock<IVeiculoRepository>().Verify(r => r.Adicionar(veiculo), Times.Never);
    }

    [Fact(DisplayName = "Adicionar Veiculo com Falha")]
    [Trait("Mock", "Veiculo Service AutoMock Tests")]
    public async Task VeiculoService_Adicionar_DeveFalharDevidoVeiculoInvalido()
    {
        // Arrange
        var veiculo = _veiculoTestsBogus.GerarVeiculoInvalido();
        var mocker = new AutoMocker();
        var veiculoService = mocker.CreateInstance<VeiculoService>();

        // Act
       await veiculoService.Adicionar(veiculo);

        // Assert
        Assert.False(veiculo.EhValido());
        mocker.GetMock<IVeiculoRepository>().Verify(r => r.Adicionar(veiculo), Times.Never);
    }
 
}
