using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoCarro.WebApp.MVC.Controllers;
using SoCarro.WebApp.MVC.Services;
using SoCarro.WebApp.MVC.ViewModels;

namespace NSE.WebApp.MVC.Controllers;

[Authorize]
public class ProprietarioController : MainController
{
    private readonly IProprietarioService _proprietarioService;

    public ProprietarioController(IProprietarioService proprietarioService)
    {
        _proprietarioService = proprietarioService;
    }

    [Route("proprietario")]
    public async Task<IActionResult> Index()
    {
        return View(await ObterStatus());
    }

    public async Task<IActionResult> Adicionar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adicionar(ProprietarioViewModel model)
    {

        if (!ModelState.IsValid) return View(model);

        var resposta = await _proprietarioService.Adicionar(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        TempData["success"] = "Proprietário salvo com sucesso";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Atualizar(Guid? id)
    {
        if (Guid.Empty == id)
            return NotFound();

        var model = await _proprietarioService.ObterPorId(id.GetValueOrDefault());

        if (model == null)
            return NotFound();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar(ProprietarioViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var resposta = await _proprietarioService.Atualizar(model);

        if (ResponsePossuiErros(resposta))
        {
            TempData["Erros"] =
            ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return View(model);
        }

        TempData["success"] = "Proprietário salvo com sucesso";
        return RedirectToAction("Index");
    }

    [HttpPut]
    public async Task<IActionResult> MudarStatus(ProprietarioViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var resposta = await _proprietarioService.Atualizar(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        return RedirectToAction("Index");
    }

    private async Task<IEnumerable<ProprietarioViewModel>> ObterStatus()
    {
        var model = (await _proprietarioService.ObterTodos()).ToList();
        foreach (var item in model) 
            item.Ativo = item.Status == 0 ? true : false;

        return model;
    }

}