using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SoCarro.Core.WebApi.Identidade;

public static class JwtConfig
{
    public static void AddJwtConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {

        //Obter as informações da section [AppSettings] que fica no arquivo appSettings.{Ambiente de Desenvolvimento}.json
        var appSettingsSection = configuration.GetSection("AppSettings");

        //Configura as informações no pipeline.
        services.Configure<AppSettings>(appSettingsSection);

        //Obtêm os dados populados.
        var appSettings = appSettingsSection.Get<AppSettings>();

        //Configura a chave de acesso do token.
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        //Configuração da autenticação do via JWT.
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        //Configuração dos parâmetros do token
        .AddJwtBearer(bearerOptions =>
        {
            bearerOptions.RequireHttpsMetadata = true;  //Acesso pelo https
            bearerOptions.SaveToken = true;             //Permitir salvar o token na instância

            //Parâmetro de validação do token
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,        //Validar  o emissor com base na assinatura.
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,                  // Validar o emissor (API)
                ValidateAudience = true,                // Validar quais domínio que são válidos
                ValidAudience = appSettings.ValidoEm,   // Qual o emissor que será válido
                ValidIssuer = appSettings.Emissor       // Onde o token será válido

                //Tanto do emissor como no local de validação, pode ser informado um array com várias informações.
                //ValidAudiences = new List<string>()
                //ValidIssuers = new List<string>()
            };
        });
    }

    public static void UseAuthConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}