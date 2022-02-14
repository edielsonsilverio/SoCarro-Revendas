using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSE.Clientes.API.Application.Events;
using SoCarro.Business.Intefaces;
using SoCarro.Business.Services;
using SoCarro.Core.DomainObjects;
using SoCarro.Core.Mediator;
using SoCarro.Core.Notificacoes;
using SoCarro.Core.WebApi.Usuario;
using SoCarro.DataAccess.Context;
using SoCarro.DataAccess.Repository;
using SoCarro.WebApi.Application.Commands;
using SoCarro.WebApi.Application.Events;
using SoCarro.WebApi.Services;


namespace SoCarro.WebApi.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services,
                                     IConfiguration configuration)
    {
        // API
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddScoped<INotificador, Notificador>();
        services.AddScoped<AutenticationService>();

        // Commands
        services.AddScoped<IRequestHandler<VeiculoEmailCommand, ValidationResult>, VeiculoEmailCommandHandler>();

        // Events
        services.AddScoped<INotificationHandler<VeiculoEmailEvent>, VeiculoEmailEventHandler>();

        // Application
        services.AddScoped<IMediatorHandler, MediatorHandler>();


        // Data
        services.AddDbContext<SoCarroDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IMarcaRepository, MarcaRepository>();
        services.AddScoped<IProprietarioRepository, ProprietarioRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();

        services.AddScoped<IMarcaService, MarcaService>();
        services.AddScoped<IProprietarioService, ProprietarioService>();
        services.AddScoped<IVeiculoService, VeiculoService>();

        return services;
    }
}
