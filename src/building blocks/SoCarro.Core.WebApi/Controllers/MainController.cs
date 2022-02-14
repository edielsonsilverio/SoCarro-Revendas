using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SoCarro.Core.DomainObjects;
using SoCarro.Core.Notificacoes;
using SoCarro.Core.WebApi.Usuario;

namespace SoCarro.Core.WebApi.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotificador _notifier;
    public readonly IAspNetUser AppUser;
    //protected ILogger Logger;
    public Guid UsuarioId { get; set; }
    protected bool UsuarioAutenticado { get; set; }

    protected ICollection<string> Erros = new List<string>();

    protected MainController(INotificador notifier,
        IAspNetUser appUser//,ILogger logger
    )
    {
        _notifier = notifier;
        AppUser = appUser;
        //Logger = logger;
        if (appUser.EstaAutenticado())
        {
            UsuarioId = appUser.ObterUserId();
            UsuarioAutenticado = true;
        }
    }

    protected bool OperacaoValida()
    {
        if(!_notifier.TemNotificacao() || !Erros.Any())
            return true;

        return false;
    }

    protected ActionResult CustomResponse(object result = null)
    {
        //if (OperacaoValida())
        //    return Ok(new { success = true, data = result });

        //return BadRequest(new { success = false, errors = _notifier.ObterNotificacoes().Select(n => n.Mensagem) });

        if (OperacaoValida())
            return Ok(result);


        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotificadorErroModelInvalida(modelState);
        return CustomResponse();
    }

    protected void NotificadorErroModelInvalida(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            AdicionarErroProcessamento(errorMsg);
        }
    }

    protected void AdicionarErroProcessamento(string mensagem)
    {
        Erros.Add(mensagem);
        _notifier.Manipulador(new Notificacao(mensagem));
    }

    protected void LimparErrosProcessamento()
    {
        Erros.Clear();
    }
}