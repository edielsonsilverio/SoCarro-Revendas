using Microsoft.AspNetCore.Mvc;
using SoCarro.Core.DomainObjects;

namespace SoCarro.WebApp.MVC.Extensions;

public class SummaryViewComponent : ViewComponent
{
    //private readonly INotificador _notificador;

    //public SummaryViewComponent(INotificador notificador) => _notificador = notificador;

    //public async Task<IViewComponentResult> InvokeAsync()
    //{
    //    var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());
    //    notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem));

    //    return View();
    //}

    public IViewComponentResult Invoke()
    {
        return View();
    }
}
 