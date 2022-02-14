﻿using Microsoft.AspNetCore.Authentication.Cookies;

namespace SoCarro.WebApp.MVC.Configurations;
public static class IdentityConfig
{
    public static void AddIdentityConfiguration(this IServiceCollection services)
    {
        //Configura a autenticação via Token
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/erro/403";
                });
    }

    public static void UseIdentityConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
