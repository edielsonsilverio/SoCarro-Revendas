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
[Route("api/proprietario")]
public class ProprietarioController : MainController
{
    private readonly IProprietarioRepository _repo;
    private readonly IMapper _mapper;
    private readonly IProprietarioService _repoService;

    public ProprietarioController(
        IProprietarioRepository repo,
        IMapper mapper,
        IProprietarioService repoService,
        IAspNetUser user,
        INotificador notificacao) : base(notificacao, user)
    {
        _repo = repo;
        _mapper = mapper;
        _repoService = repoService;
    }

    [ClaimsAuthorize("Proprietario", "R")]
    [HttpGet]
    [Route("obtertodos")]
    public async Task<IActionResult> ObterTodos()
    {
        var lista = _mapper.Map<IEnumerable<ProprietarioViewModel>>(await _repo.ObterTodos()).ToList();
        return Ok(lista);
    }

    [ClaimsAuthorize("Proprietario", "R")]
    [HttpGet]
    [Route("consultar-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var objeto = _mapper.Map<ProprietarioViewModel>(await ObterProprietario(id));
        return Ok(objeto);
    }

    [ClaimsAuthorize("Proprietario", "C")]
    [HttpPost]
    public async Task<ActionResult<ProprietarioViewModel>> Adicionar([FromBody] ProprietarioViewModel proprietarioViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (!await _repoService.Adicionar(_mapper.Map<Proprietario>(proprietarioViewModel)))
            return CustomResponse(ModelState);

        await PersistirDados();
        return CustomResponse(proprietarioViewModel);
    }

    [ClaimsAuthorize("Proprietario", "U")]
    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<ProprietarioViewModel>> Atualizar(Guid id, ProprietarioViewModel proprietarioViewModel)
    {
        if (id != proprietarioViewModel?.Id)
        {
            AdicionarErroProcessamento("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(proprietarioViewModel);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (!await _repoService.Atualizar(_mapper.Map<Proprietario>(proprietarioViewModel)))
            return CustomResponse(ModelState);

        await PersistirDados();

        return CustomResponse(proprietarioViewModel);
    }

    [ClaimsAuthorize("Proprietario", "U")]
    [HttpPut("trocar-status/{id}")]
    public async Task<ActionResult<MarcaViewModel>> TrocarStatus(Guid id, ProprietarioViewModel proprietarioViewModel)
    {
        if (id != proprietarioViewModel?.Id)
        {
            AdicionarErroProcessamento("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(proprietarioViewModel);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var marca = await _repo.ObterPorId(proprietarioViewModel.Id);

        if (marca.Status == ObterTipoStatus(proprietarioViewModel.Status))
        {
            AdicionarErroProcessamento("Não é possivel salvar o mesmo status");
            return CustomResponse(proprietarioViewModel);
        }

        if (marca.Status == TipoStatus.Ativo)
            marca.Cancelar();
        else
            marca.Ativar();

        if (!await _repoService.Atualizar(marca))
            return CustomResponse(ModelState);

        await PersistirDados();
        return CustomResponse(proprietarioViewModel);
    }
    private async Task<ProprietarioViewModel> ObterProprietario(Guid id)
    {
        return _mapper.Map<ProprietarioViewModel>(await _repo.ObterPorId(id));
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
