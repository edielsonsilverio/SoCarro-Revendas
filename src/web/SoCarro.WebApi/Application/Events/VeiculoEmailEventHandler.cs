using MediatR;
using SoCarro.WebApi.Application.Events;

namespace NSE.Clientes.API.Application.Events;
public class VeiculoEmailEventHandler : INotificationHandler<VeiculoEmailEvent>
{
    public Task Handle(VeiculoEmailEvent notification, CancellationToken cancellationToken)
    {
        // Enviar evento de confirmação
        return Task.CompletedTask;
    }
}