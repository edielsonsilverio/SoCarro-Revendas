using SoCarro.Core.Messages.Integration;
using SoCarro.WebApi.ViewModels;

namespace SoCarro.WebApi.Application.Events;

public class VeiculoEmailEvent : IntegrationEvent
{
    public EmailVeiculoViewModel VeiculoEmail { get; set; }

    public VeiculoEmailEvent(Guid id, EmailVeiculoViewModel veiculoEmail)
    {
        AggregateId = id;
        VeiculoEmail = veiculoEmail;
    }
}