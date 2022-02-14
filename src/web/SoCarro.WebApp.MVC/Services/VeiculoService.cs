using Microsoft.Extensions.Options;
using SoCarro.Core.Comunication;
using SoCarro.WebApp.MVC.Extensions;
using SoCarro.WebApp.MVC.ViewModels;

namespace SoCarro.WebApp.MVC.Services;

public class VeiculoService : Service, IVeiculoService
{
    private readonly HttpClient _httpClient;

    public VeiculoService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.ApiUrl);

        _httpClient = httpClient;
    }

    public async Task<VeiculoViewModel> ObterPorId(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/veiculo/consultar-id/{id}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<VeiculoViewModel>(response);
    }

    public async Task<ModeloViewModel> ObterModeloPorId(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/veiculo/modelo-consultar-id/{id}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<ModeloViewModel>(response);
    }

    public async Task<IEnumerable<VeiculoViewModel>> ObterTodos()
    {
        var response = await _httpClient.GetAsync($"/api/veiculo/obtertodos/");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<IEnumerable<VeiculoViewModel>>(response);
    }

    public async Task<ResponseResult> Adicionar(VeiculoViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PostAsync("/api/veiculo/", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }

    public async Task<ResponseResult> Atualizar(VeiculoViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PutAsync($"/api/veiculo/{model.Id}", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }
    public async Task<ResponseResult> MudarStatus(VeiculoViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PutAsync($"/api/veiculo/trocar-status/{model.Id}", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }
}