using Moq;
using SoCarro.Business.Intefaces;
using Xunit;

namespace SoCarro.Test;

[Collection(nameof(VeiculoAutoMockerCollection))]
public class VeiculoServiceAutoMockerFixtureTests
{
    readonly VeiculoTestsAutoMockerFixture _veiculoTestsAutoMockerFixture;

    private readonly VeiculoService _veiculoService;

    public VeiculoServiceAutoMockerFixtureTests(VeiculoTestsAutoMockerFixture veiculoTestsFixture)
    {
        _veiculoTestsAutoMockerFixture = veiculoTestsFixture;
        _veiculoService = _veiculoTestsAutoMockerFixture.ObterVeiculoService();
    }

    [Fact(DisplayName = "Adicionar Veiculo com Sucesso")]
    [Trait("Mock", "Veiculo Service AutoMockFixture Tests")]
    public async void VeiculoService_Adicionar_DeveExecutarComSucesso()
    {
        // Arrange
        var veiculo = _veiculoTestsAutoMockerFixture.GerarVeiculoValido();

        // Act
        await _veiculoService.Adicionar(veiculo);

        // Assert
        Assert.True(veiculo.Validar());
        _veiculoTestsAutoMockerFixture.Mocker.GetMock<IVeiculoRepository>().Verify(r => r.Adicionar(veiculo), Times.Never);

    }

    [Fact(DisplayName = "Adicionar Veiculo com Falha")]
    [Trait("Mock", "Veiculo Service AutoMockFixture Tests")]
    public async void VeiculoService_Adicionar_DeveFalharDevidoVeiculoInvalido()
    {
        // Arrange
        var veiculo = _veiculoTestsAutoMockerFixture.GerarVeiculoInvalido();

        // Act
        await _veiculoService.Adicionar(veiculo);

        // Assert
        Assert.True(veiculo.Validar());
        _veiculoTestsAutoMockerFixture.Mocker.GetMock<IVeiculoRepository>().Verify(r => r.Adicionar(veiculo), Times.Never);
        
    }
}