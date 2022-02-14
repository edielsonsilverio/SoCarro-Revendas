using Microsoft.Extensions.Options;
using SoCarro.Core.Comunication;
using SoCarro.WebApp.MVC.Extensions;
using SoCarro.WebApp.MVC.ViewModels;

namespace SoCarro.WebApp.MVC.Services;

public class MarcaService : Service, IMarcaService
{
    private readonly HttpClient _httpClient;

    public MarcaService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.ApiUrl);

        _httpClient = httpClient;
    }

    public async Task<MarcaViewModel> ObterPorId(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/marca/consultar-id/{id}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<MarcaViewModel>(response);
    }

    public async Task<IEnumerable<MarcaViewModel>> ObterTodos()
    {
        var response = await _httpClient.GetAsync($"/api/marca/obtertodos/");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<IEnumerable<MarcaViewModel>>(response);
    }

    public async Task<ResponseResult> Adicionar(MarcaViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PostAsync("/api/marca/", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }

    public async Task<ResponseResult> Atualizar(MarcaViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PutAsync($"/api/marca/{model.Id}", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }
    public async Task<ResponseResult> MudarStatus(MarcaViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PutAsync($"/api/marca/trocar-status/{model.Id}", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }
}