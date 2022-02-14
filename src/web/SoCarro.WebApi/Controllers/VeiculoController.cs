using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoCarro.Business.Intefaces;
using SoCarro.Business.Models;
using SoCarro.Core;
using SoCarro.Core.DomainObjects;
using SoCarro.Core.Messages.Integration;
using SoCarro.Core.WebApi.Controllers;
using SoCarro.Core.WebApi.Identidade;
using SoCarro.Core.WebApi.Usuario;
using SoCarro.MessageBus;
using SoCarro.WebApi.ViewModels;

namespace SoCarro.WebApi.Controllers;

[Authorize]
[Route("api/veiculo")]
public class VeiculoController : MainController
{
    private readonly IVeiculoRepository _repo;
    private readonly IMapper _mapper;
    private readonly IVeiculoService _repoService;
    private readonly IMessageBus _bus;
    public VeiculoController(
        IVeiculoRepository repo,
        IMapper mapper,
        IVeiculoService repoService,
        IAspNetUser user,
        INotificador notificacao,
        IMessageBus bus) : base(notificacao, user)
    {
        _repo = repo;
        _mapper = mapper;
        _repoService = repoService;
        _bus = bus;
    }

    [ClaimsAuthorize("Veiculo", "R")]
    [HttpGet]
    [Route("obtertodos")]
    public async Task<IActionResult> ObterTodos()
    {
        return Ok(_mapper.Map<IEnumerable<VeiculoViewModel>>(await _repo.ObterTodos()));
    }

    [ClaimsAuthorize("Veiculo", "R")]
    [HttpGet]
    [Route("consultar-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        return Ok(await ObterVeiculo(id));
    }

    [ClaimsAuthorize("Veiculo", "R")]
    [HttpGet]
    [Route("modelo-consultar-id/{id}")]
    public async Task<IActionResult> ObterModeloPorId(Guid id)
    {
        return Ok(await _repo.ObterModeloPorId(id));
    }

    [ClaimsAuthorize("Veiculo", "C")]
    [HttpPost]
    public async Task<ActionResult<VeiculoViewModel>> Adicionar([FromBody] VeiculoViewModel veiculoViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        //Adicionar pelo repositorio
        //return await AdicionarVeiculoRepositorio(veiculoViewModel);

        if(veiculoViewModel.Proprietario == null)
            veiculoViewModel.Proprietario = _mapper.Map<ProprietarioViewModel>(await _repo.ObterProprietarioPorId(veiculoViewModel.ProprietarioId));
      
        //Enviando mensagem e cadastrando
        return await AdicionarPorMensagem(veiculoViewModel);
    }



    [ClaimsAuthorize("Veiculo", "U")]
    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<VeiculoViewModel>> Atualizar(Guid id, VeiculoViewModel veiculoViewModel)
    {
        if (id != veiculoViewModel?.Id)
        {
            AdicionarErroProcessamento("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(veiculoViewModel);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var marca = await _repo.ObterPorId(veiculoViewModel.Id);
        if (!VerificarStatus(marca.Status, ObterTipoStatus(veiculoViewModel.Status)))
            return CustomResponse(veiculoViewModel);

        var model = _mapper.Map<Veiculo>(veiculoViewModel);

        if (!await _repoService.Atualizar(model))
            return CustomResponse(ModelState);

        await PersistirDados();

        return CustomResponse(veiculoViewModel);
    }

    [ClaimsAuthorize("Veiculo", "U")]
    [HttpPut("trocar-status/{id}")]
    public async Task<ActionResult<MarcaViewModel>> TrocarStatus(Guid id, VeiculoViewModel veiculoViewModel)
    {
        if (id != veiculoViewModel?.Id)
        {
            AdicionarErroProcessamento("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(veiculoViewModel);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var marca = await _repo.ObterPorId(veiculoViewModel.Id);

        if (!VerificarStatus(marca.Status, ObterTipoStatus(veiculoViewModel.Status)))
            return CustomResponse(veiculoViewModel);

        if (marca.Status == ObterTipoStatus(veiculoViewModel.Status))
        {
            AdicionarErroProcessamento("Não é possivel salvar o mesmo status");
            return CustomResponse(veiculoViewModel);
        }

        if (!await _repoService.Atualizar(marca))
            return CustomResponse(ModelState);

        await PersistirDados();
        return CustomResponse(veiculoViewModel);
    }
    private async Task<VeiculoViewModel> ObterVeiculo(Guid id)
    {
        return _mapper.Map<VeiculoViewModel>(await _repo.ObterPorId(id));
    }

    private TipoStatusVeiculo ObterTipoStatus(int tipo)
    {
        return tipo == 0 ? TipoStatusVeiculo.Disponivel : tipo == 1 ? TipoStatusVeiculo.Indisponivel : TipoStatusVeiculo.Vendido;
    }
    private bool VerificarStatus(TipoStatusVeiculo tipoModel, TipoStatusVeiculo tipoViewModel)
    {
        if (tipoModel == TipoStatusVeiculo.Vendido && tipoViewModel != TipoStatusVeiculo.Vendido)
        {
            AdicionarErroProcessamento("Não é possível mudar o status de vendido para outro tipo");
            return false;
        }
        return true;
    }
    private async Task PersistirDados()
    {
        var result = await _repo.PersistirDados();
        if (!string.IsNullOrEmpty(result))
            AdicionarErroProcessamento(result);
    }

    private async Task<ActionResult<VeiculoViewModel>> AdicionarVeiculoRepositorio(VeiculoViewModel veiculoViewModel)
    {
        var model = _mapper.Map<Veiculo>(veiculoViewModel);
        if (!await _repoService.Adicionar(model))
        {
            AdicionarErroProcessamento("Erro ao tentar salvar o registro.");
            return CustomResponse(ModelState);
        }

        await PersistirDados();
        return CustomResponse(veiculoViewModel);
    }

    private async Task<ActionResult<VeiculoViewModel>> AdicionarPorMensagem(VeiculoViewModel veiculoViewModel)
    {
        var model = _mapper.Map<Veiculo>(veiculoViewModel);

        var message = new EmailVeiculoViewModel()
        {
            Assunto = "Enviando email - Veiculo Adicionado",
            Destino = model.Proprietario.Email.Endereco,
            Mensagem = $"Olá {model.Proprietario.Nome} seu veículo foi adicionado com sucesso",
            Origem = GlobalConstants.EMAIL_EMPRESA,
            Veiculo = veiculoViewModel
        };
        var resultado = await RegistrarVeiculo(message);

        if (!resultado.ValidationResult.IsValid)
        {
            AdicionarErroProcessamento("Erro ao tentar salvar o registro.");
            return CustomResponse(ModelState);
        }

        return CustomResponse(veiculoViewModel);
    }

    private async Task<ResponseMessage> RegistrarVeiculo(EmailVeiculoViewModel message)
    {

        var emailEvent = new EnviarEmailIntegrationEvent(message.Origem, message.Destino, message.Assunto, message.Mensagem);
        try
        {
            return await _bus.RequestAsync<EnviarEmailIntegrationEvent, ResponseMessage>(emailEvent);
        }
        catch(Exception ex)
        {
            var error = ex.Message;
            return null;
        }
    }
}

