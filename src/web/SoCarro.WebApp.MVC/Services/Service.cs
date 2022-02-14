using SoCarro.WebApp.MVC.Extensions;
using System.Text;
using SoCarro.Core.Comunication;

namespace SoCarro.WebApp.MVC.Services;

public abstract class Service
{
    #region System.Text.Json
    protected StringContent ObterConteudo(object dado)
    {
        return new StringContent(
            System.Text.Json.JsonSerializer.Serialize(dado),
            Encoding.UTF8,
            "application/json");
    }

    protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
    {
        var options = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return System.Text.Json.JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
    }
    #endregion


    protected bool TratarErrosResponse(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpRequestException(response.StatusCode);

            case 400:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }

    protected ResponseResult RetornoOk()
    {
        return new ResponseResult();
    }
}

