using Bogus;
using Bogus.DataSets;
using Moq.AutoMock;
using SoCarro.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SoCarro.Test;

[CollectionDefinition(nameof(VeiculoAutoMockerCollection))]
public class VeiculoAutoMockerCollection : ICollectionFixture<VeiculoTestsAutoMockerFixture>
{ }

public class VeiculoTestsAutoMockerFixture : IDisposable
{
    public VeiculoService VeiculoService;
    public AutoMocker Mocker;

    public Veiculo GerarClienteValido()
    {
        return GerarVeiculos(1, true).FirstOrDefault();
    }

    public IEnumerable<Veiculo> ObterVeiculos()
    {
        var clientes = new List<Veiculo>();

        clientes.AddRange(GerarVeiculos(50, true).ToList());
        clientes.AddRange(GerarVeiculos(50, false).ToList());

        return clientes;
    }

    public Veiculo GerarVeiculoValido()
    {
        return GerarVeiculos(1, true).FirstOrDefault();
    }

    public IEnumerable<Veiculo> GerarVeiculos(int quantidade, bool ativo)
    {
        var genero = new Faker().PickRandom<Name.Gender>();

        var model = new Faker<Veiculo>("pt_BR")
            .CustomInstantiator(f => new Veiculo(
                nome: f.Vehicle.Locale.FirstOrDefault().ToString(),
                renavam: "67118039920",
                quilometragem:16520,
                valor: 1566825.00m,
                marcaId: Guid.Empty,
                proprietarioId:Guid.Empty));

        return model.Generate(quantidade);
    }

    public Veiculo GerarVeiculoInvalido()
    {
        var genero = new Faker().PickRandom<Name.Gender>();

        var modelo = new Faker<Veiculo>("pt_BR")
             .CustomInstantiator(f => new Veiculo(
                nome: f.Vehicle.Locale.FirstOrDefault().ToString(),
                renavam: "67118039920",
                quilometragem: 16520,
                valor: 1566825.00m,
                marcaId: Guid.Empty,
                proprietarioId: Guid.Empty));

        return modelo;
    }

    public VeiculoService ObterVeiculoService()
    {
        Mocker = new AutoMocker();
        VeiculoService = Mocker.CreateInstance<VeiculoService>();

        return VeiculoService;
    }

    public void Dispose()
    {
    }
}
