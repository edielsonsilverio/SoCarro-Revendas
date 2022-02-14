using MediatR;
using NSE.Identidade.API.Configuration;
using SoCarro.Core.WebApi.Identidade;
using SoCarro.WebApi.Configuration;

namespace SoCarro.WebApi;
public class Startup : SoCarro.Core.WebApi.IStartup
{
    public IConfiguration Configuration { get; }

    public Startup(IHostEnvironment hostEnvironment)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        if (hostEnvironment.IsDevelopment())
            builder.AddUserSecrets<Startup>();

        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiConfiguration();

        services.AddIdentityConfiguration(Configuration);

        services.AddAutoMapper(typeof(Startup));

        services.AddJwtConfiguration(Configuration);

        services.AddSwaggerConfiguration();

        services.AddMediatR(typeof(Startup));

        services.AddMessageBusConfiguration(Configuration);

        services.RegisterServices(Configuration);
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseSwaggerConfiguration();

        app.UseApiConfiguration(env);
    }
}
