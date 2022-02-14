using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SoCarro.Core;
using SoCarro.Core.Messages.Integration;
using SoCarro.MessageBus;
using SoCarro.WebApp.MVC.Controllers;
using SoCarro.WebApp.MVC.Services;
using SoCarro.WebApp.MVC.ViewModels;

namespace NSE.WebApp.MVC.Controllers;

[Authorize]
public class VeiculoController : MainController
{
    private readonly IVeiculoService _veiculoService;
    private readonly IMarcaService _marcaService;
    private readonly IProprietarioService _proprietarService;

    public VeiculoController(IVeiculoService veiculoService,
                             IMarcaService marcaService,
                             IProprietarioService proprietarService)
    {
        _veiculoService = veiculoService;
        _marcaService = marcaService;
        _proprietarService = proprietarService;
    }

    [Route("veiculo")]
    public async Task<IActionResult> Index()
    {
        return View(await ObterStatus());
    }

    public async Task<IActionResult> Adicionar()
    {
        var model = new VeiculoViewModel
        {
            Modelo = new ModeloViewModel(),
            Marcas = await ObterTodasMarcas(),
            Proprietarios = await ObterTodosProprietarios()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adicionar(VeiculoViewModel model)
    {

        if (!ModelState.IsValid) return View(model);

        var resposta = await _veiculoService.Adicionar(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        TempData["success"] = "Veículo salvo com sucesso";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Atualizar(Guid? id)
    {
        if (Guid.Empty == id)
            return NotFound();

        var model = await _veiculoService.ObterPorId(id.GetValueOrDefault());

        if (model == null)
            return NotFound();

        model.Marcas = await ObterTodasMarcas();
        model.Proprietarios = await ObterTodosProprietarios();

        if (model.Modelo == null)
            model.Modelo = await _veiculoService.ObterModeloPorId(id.GetValueOrDefault());

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar(VeiculoViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var resposta = await _veiculoService.Atualizar(model);

        if (ResponsePossuiErros(resposta))
        {
            TempData["Erros"] =
            ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return View(model);
        }

        TempData["success"] = "Veículo salvo com sucesso";
        return RedirectToAction("Index");
    }

    [HttpPut]
    public async Task<IActionResult> MudarStatus(VeiculoViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var resposta = await _veiculoService.Atualizar(model);

        if (ResponsePossuiErros(resposta))
        {
            TempData["Erros"] =
            ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return View(model);
        }

        return RedirectToAction("Index");
    }

    private async Task<IEnumerable<VeiculoViewModel>> ObterStatus()
    {
        var model = (await _veiculoService.ObterTodos());
        //foreach (var item in model) 
        //    item.Ativo = item.Status == 0 ? true : false;

        return model;
    }

    public async Task<IEnumerable<SelectListItem>> ObterTodasMarcas()
    {
        return (await _marcaService.ObterTodos()).Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });
    }

    public async Task<IEnumerable<SelectListItem>> ObterTodosProprietarios()
    {
        return (await _proprietarService.ObterTodos()).Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });
    }

}