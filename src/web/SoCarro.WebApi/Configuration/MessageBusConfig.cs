using SoCarro.Core.Utils;
using SoCarro.MessageBus;

namespace SoCarro.WebApi.Configuration;
public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
    }
}