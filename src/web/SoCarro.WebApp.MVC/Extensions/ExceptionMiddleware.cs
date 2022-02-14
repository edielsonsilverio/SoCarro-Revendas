using System.Net;
using Polly.CircuitBreaker;
using SoCarro.WebApp.MVC.Services;

namespace SoCarro.WebApp.MVC.Extensions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private static IAutenticacaoService _autenticacaoService;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IAutenticacaoService autenticacaoService)
    {
        //A injeção tem que ser feita dentro do método invoke, pois o middle é singleton
        // e a autentication é scope, por isso não pode ser injetado no construtor.
        _autenticacaoService = autenticacaoService;
        try
        {
            await _next(httpContext);
        }
        catch (CustomHttpRequestException ex)
        {
            var error = ex.Message;
            HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        }
        catch (BrokenCircuitException ex)
        {
            var erro = ex.Message;
            HandleRequestBrokenCircuitExceptionAsync(httpContext);
        }
    }

    private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
    {
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            _autenticacaoService.Logout();

            //Verifica o status 401 e retorna para o Login com a url que tentou acessar.
            context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
            return;
        }

        //Retorna o status code para ser tratado.
        context.Response.StatusCode = (int)statusCode;
    }

    private static void HandleRequestBrokenCircuitExceptionAsync(HttpContext context)
    {
       
            //Verifica o status 401 e retorna para o Login com a url que tentou acessar.
            context.Response.Redirect($"/sistema-indisponivel");
    }
}

