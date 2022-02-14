using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoCarro.WebApp.MVC.Controllers;
using SoCarro.WebApp.MVC.Services;
using SoCarro.WebApp.MVC.ViewModels;

namespace NSE.WebApp.MVC.Controllers;

[Authorize]
public class MarcaController : MainController
{
    private readonly IMarcaService _marcaService;

    public MarcaController(IMarcaService marcaService)
    {
        _marcaService = marcaService;
    }

    [Route("marca")]
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
    public async Task<IActionResult> Adicionar(MarcaViewModel model)
    {

        if (!ModelState.IsValid) return View(model);

        var resposta = await _marcaService.Adicionar(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        TempData["success"] = "Marca salva com sucesso";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Atualizar(Guid? id)
    {
        if (Guid.Empty == id)
            return NotFound();

        var model = await _marcaService.ObterPorId(id.GetValueOrDefault());

        if (model == null)
            return NotFound();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar(MarcaViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var resposta = await _marcaService.Atualizar(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        TempData["success"] = "Marca salva com sucesso";
        return RedirectToAction("Index");
    }

    [HttpPut]
    public async Task<IActionResult> MudarStatus(MarcaViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var resposta = await _marcaService.Atualizar(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        return RedirectToAction("Index");
    }

    private async Task<IEnumerable<MarcaViewModel>> ObterStatus()
    {
        var model = (await _marcaService.ObterTodos()).ToList();
        foreach (var item in model) 
            item.Ativo = item.Status == 0 ? true : false;

        return model;
    }

}