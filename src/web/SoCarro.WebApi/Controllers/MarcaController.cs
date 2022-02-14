using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using SoCarro.Core.DomainObjects;
using SoCarro.Core.WebApi.Controllers;
using SoCarro.Core.WebApi.Identidade;
using SoCarro.Core.WebApi.Usuario;
using SoCarro.WebApi.ViewModels;

namespace SoCarro.WebApi.Controllers;

[Authorize]
[Route("api/marca")]
public class MarcaController : MainController
{
    private readonly IMarcaRepository _repo;
    private readonly IMapper _mapper;
    private readonly IMarcaService _repoService;

    public MarcaController(
        IMarcaRepository repo,
        IMapper mapper,
        IMarcaService repoService,
        IAspNetUser user,
        INotificador notificacao) : base(notificacao, user)
    {
        _repo = repo;
        _mapper = mapper;
        _repoService = repoService;
    }

    [ClaimsAuthorize("Marca", "R")]
    [HttpGet]
    [Route("obtertodos")]
    public async Task<IActionResult> ObterTodos()
    {
        var lista = _mapper.Map<IEnumerable<MarcaViewModel>>(await _repo.ObterTodos()).ToList();
        return Ok(lista);
    }

    [ClaimsAuthorize("Marca", "R")]
    [HttpGet]
    [Route("consultar-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var objeto = _mapper.Map<MarcaViewModel>(await ObterMarca(id));
        return Ok(objeto);
    }

    [ClaimsAuthorize("Marca", "C")]
    [HttpPost]
    public async Task<ActionResult<MarcaViewModel>> Adicionar([FromBody] MarcaViewModel marcaViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (!await _repoService.Adicionar(_mapper.Map<Marca>(marcaViewModel)))
            return CustomResponse(ModelState);

        await PersistirDados();

        return CustomResponse(marcaViewModel);
    }

    [ClaimsAuthorize("Marca", "U")]
    [HttpPut("trocar-status/{id}")]
    public async Task<ActionResult<MarcaViewModel>> TrocarStatus(Guid id, MarcaViewModel marcaViewModel)
    {
        if (id != marcaViewModel?.Id)
        {
            AdicionarErroProcessamento("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(marcaViewModel);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var marca = await _repo.ObterPorId(marcaViewModel.Id);

        if (marca.Status == ObterTipoStatus(marcaViewModel.Status))
        {
            AdicionarErroProcessamento("Não é possivel salvar o mesmo status");
            return CustomResponse(marcaViewModel);
        }

        if (marca.Status == TipoStatus.Ativo)
            marca.Cancelar();
        else
            marca.Ativar();

        if (!await _repoService.Atualizar(marca))
            return CustomResponse(ModelState);

        await PersistirDados();

        return CustomResponse(marcaViewModel);
    }

    [ClaimsAuthorize("Marca", "U")]
    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<MarcaViewModel>> Atualizar(Guid id, MarcaViewModel marcaViewModel)
    {
        if (id != marcaViewModel?.Id)
        {
            AdicionarErroProcessamento("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(marcaViewModel);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (!await _repoService.Atualizar(_mapper.Map<Marca>(marcaViewModel)))
            return CustomResponse(ModelState);

        await PersistirDados();

        return CustomResponse(marcaViewModel);
    }

    private async Task<MarcaViewModel> ObterMarca(Guid id)
    {
        return _mapper.Map<MarcaViewModel>(await _repo.ObterPorId(id));
    }

    private TipoStatus ObterTipoStatus(int tipo)
    {
        return tipo == 0 ? TipoStatus.Ativo : TipoStatus.Cancelado;
    }

    private async Task PersistirDados()
    {
        var result = await _repo.PersistirDados();
        if (!string.IsNullOrEmpty(result))
            AdicionarErroProcessamento(result);
    }
}
