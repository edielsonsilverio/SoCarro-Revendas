using System.Globalization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using SoCarro.WebApp.MVC.Extensions;

namespace SoCarro.WebApp.MVC.Configurations;

public static class WebAppConfig
{
    public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews().AddRazorRuntimeCompilation();

        services.Configure<AppSettings>(configuration);
    }
    public static void UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {

        //Middware que captura os erros não tratados
        app.UseExceptionHandler("/erro/500");

        //Captura os erros que retornaram algum código de erro.
        app.UseStatusCodePagesWithRedirects("/erro/{0}");
        app.UseHsts();

        Syncfusion.Licensing.SyncfusionLicenseProvider
        .RegisterLicense("NTc5ODAwQDMxMzkyZTM0MmUzME1BRG4xTXA0YjVSYnY1TGU3NWZRZk1mcWhGY2xNRGdXRHRYL0FBMk02b1k9");


        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        //Está opção deve ficar entre o UseRouting e o UseEndPoint
        app.UseIdentityConfiguration();


        //Configura a cultura do Browser e da tela
        var suportedCultures = new[] { new CultureInfo("pt-BR") };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = suportedCultures,
            SupportedUICultures = suportedCultures
        });

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}