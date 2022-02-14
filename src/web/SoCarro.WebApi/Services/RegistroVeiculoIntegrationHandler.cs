using FluentValidation.Results;
using SoCarro.Core.Mediator;
using SoCarro.Core.Messages.Integration;
using SoCarro.MessageBus;
using SoCarro.WebApi.Application.Commands;
using SoCarro.WebApi.Application.Events;

namespace SoCarro.WebApi.Services;

public class RegistroVeiculoIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegistroVeiculoIntegrationHandler(
                    IServiceProvider serviceProvider,
                    IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }

    private void SetResponder()
    {
        _bus.RespondAsync<VeiculoEmailEvent, ResponseMessage>(async request =>
           await RegistrarVeiculo(request));

        _bus.AdvancedBus.Connected += OnConnect;
    }

    private void OnConnect(object s, EventArgs e)
    {
        SetResponder();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        return Task.CompletedTask;
    }


    private async Task<ResponseMessage> RegistrarVeiculo(VeiculoEmailEvent message)
    {
        var command = new VeiculoEmailCommand(message.VeiculoEmail.Veiculo.Id, message.VeiculoEmail);
        ValidationResult sucesso;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            sucesso = await mediator.EnviarComando(command);
        }

        return new ResponseMessage(sucesso);
    }
}

