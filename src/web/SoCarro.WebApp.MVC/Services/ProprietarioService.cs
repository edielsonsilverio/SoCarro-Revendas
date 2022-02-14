using Microsoft.Extensions.Options;
using SoCarro.Core.Comunication;
using SoCarro.WebApp.MVC.Extensions;
using SoCarro.WebApp.MVC.ViewModels;

namespace SoCarro.WebApp.MVC.Services;

public class ProprietarioService : Service, IProprietarioService
{
    private readonly HttpClient _httpClient;

    public ProprietarioService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.ApiUrl);

        _httpClient = httpClient;
    }

    public async Task<ProprietarioViewModel> ObterPorId(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/proprietario/consultar-id/{id}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<ProprietarioViewModel>(response);
    }

    public async Task<IEnumerable<ProprietarioViewModel>> ObterTodos()
    {
        var response = await _httpClient.GetAsync($"/api/proprietario/obtertodos/");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<IEnumerable<ProprietarioViewModel>>(response);
    }

    public async Task<ResponseResult> Adicionar(ProprietarioViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PostAsync("/api/proprietario/", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }

    public async Task<ResponseResult> Atualizar(ProprietarioViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PutAsync($"/api/proprietario/{model.Id}", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }
    public async Task<ResponseResult> MudarStatus(ProprietarioViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PutAsync($"/api/proprietario/trocar-status/{model.Id}", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }
}