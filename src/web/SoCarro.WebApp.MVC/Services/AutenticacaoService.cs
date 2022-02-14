using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using SoCarro.Core.Comunication;
using SoCarro.Core.WebApi.Usuario;
using SoCarro.WebApp.MVC.Extensions;
using SoCarro.WebApp.MVC.ViewModels;

namespace SoCarro.WebApp.MVC.Services;

public class AutenticacaoService : Service, IAutenticacaoService
{
    private readonly HttpClient _httpClient;

    private readonly IAspNetUser _user;
    private readonly IAuthenticationService _authenticationService;

    public AutenticacaoService(HttpClient httpClient,
                               IOptions<AppSettings> settings, 
                               IAspNetUser user, 
                               IAuthenticationService authenticationService)
    {
        httpClient.BaseAddress = new Uri(settings.Value.ApiUrl);

        _httpClient = httpClient;
        _user = user;
        _authenticationService = authenticationService;
    }

    public async Task<LoginResponseViewModel> Login(LoginUserViewModel usuarioLogin)
    {
        var loginContent = ObterConteudo(usuarioLogin);

        var response = await _httpClient.PostAsync("/api/entrar", loginContent);

        if (!TratarErrosResponse(response))
        {
            return new LoginResponseViewModel
            {
                ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
            };
        }

        return await DeserializarObjetoResponse<LoginResponseViewModel>(response);
    }

    public async Task<LoginResponseViewModel> Registro(RegisterUserViewModel usuarioRegistro)
    {
        var registroContent = ObterConteudo(usuarioRegistro);

        var response = await _httpClient.PostAsync("/api/nova-conta", registroContent);

        if (!TratarErrosResponse(response))
        {
            return new LoginResponseViewModel
            {
                ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
            };
        }

        return await DeserializarObjetoResponse<LoginResponseViewModel>(response);
    }

    public async Task<LoginResponseViewModel> UtilizarRefreshToken(string refreshToken)
    {
        var refreshTokenContent = ObterConteudo(refreshToken);

        var response = await _httpClient.PostAsync("/api/identidade/refresh-token", refreshTokenContent);

        if (!TratarErrosResponse(response))
        {
            return new LoginResponseViewModel
            {
                ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
            };
        }

        return await DeserializarObjetoResponse<LoginResponseViewModel>(response);
    }

    public async Task RealizarLogin(LoginResponseViewModel resposta)
    {
        var token = ObterTokenFormatado(resposta.AccessToken);

        var claims = new List<Claim>();
        claims.Add(new Claim("JWT", resposta.AccessToken));
        claims.AddRange(token.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
            IsPersistent = true
        };

        await _authenticationService.SignInAsync(
            _user.ObterHttpContext(),
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    public async Task Logout()
    {
        await _authenticationService.SignOutAsync(
            _user.ObterHttpContext(),
            CookieAuthenticationDefaults.AuthenticationScheme,
            null);
    }

    public static JwtSecurityToken ObterTokenFormatado(string jwtToken)
    {
        //Formata a string do token recebida em um formato JWT
        return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }

    public bool TokenExpirado()
    {
        var jwt = _user.ObterUserToken();
        if (jwt is null) return false;

        var token = ObterTokenFormatado(jwt);
        return token.ValidTo.ToLocalTime() < DateTime.Now;
    }
}

