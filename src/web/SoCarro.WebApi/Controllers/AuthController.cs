using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoCarro.Core.DomainObjects;
using SoCarro.Core.WebApi.Controllers;
using SoCarro.Core.WebApi.Usuario;
using SoCarro.WebApi.Services;
using SoCarro.WebApi.ViewModels;

namespace SoCarro.WebApi.Controllers;

[Route("api/")]
public class AuthController : MainController
{
    private AutenticationService _authenticationService;
    private readonly ILogger _logger;

    public AuthController(INotificador notificador,
                          AutenticationService authenticationService,
                          IAspNetUser user, ILogger<AuthController> logger) : base(notificador, user)
    {
        _authenticationService = authenticationService;
        _logger = logger;
    }

    //[EnableCors("Development")]
    [HttpPost("nova-conta")]
    public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await _authenticationService.UserManager.CreateAsync(user, registerUser.Password);
        if (result.Succeeded)
        {
            await _authenticationService.SignInManager.SignInAsync(user, false);
            return CustomResponse(await _authenticationService.GerarJwt(user.Email));
        }
        foreach (var error in result.Errors)
        {
            AdicionarErroProcessamento(error.Description);
        }

        return CustomResponse(registerUser);
    }

    [HttpPost("entrar")]
    public async Task<ActionResult> Login(LoginUserViewModel loginUser)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _authenticationService.SignInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        if (result.Succeeded)
        {
            _logger.LogInformation("Usuario " + loginUser.Email + " logado com sucesso");
            var token = await _authenticationService.GerarJwt(loginUser.Email);
            return CustomResponse(await _authenticationService.GerarJwt(loginUser.Email));
        }
        if (result.IsLockedOut)
        {
            AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
            return CustomResponse(loginUser);
        }

        AdicionarErroProcessamento("Usuário ou Senha incorretos");
        return CustomResponse(loginUser);
    }
  }
