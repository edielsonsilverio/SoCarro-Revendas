using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoCarro.Core.WebApi.Identidade;
using SoCarro.Core.WebApi.Usuario;
using SoCarro.WebApi.Data;
using SoCarro.WebApi.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoCarro.WebApi.Services;

public class AutenticationService
{
    public readonly SignInManager<IdentityUser> SignInManager;
    public readonly UserManager<IdentityUser> UserManager;
    private readonly AppSettings _appSettings;
    private readonly AppTokenSettings _appTokenSettingsSettings;
    private readonly ApplicationDbContext _context;

    private readonly IAspNetUser _aspNetUser;

    public AutenticationService(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IOptions<AppSettings> appSettings,
        IOptions<AppTokenSettings> appTokenSettingsSettings,
        ApplicationDbContext context,
        IAspNetUser aspNetUser)
    {
        SignInManager = signInManager;
        UserManager = userManager;
        _appSettings = appSettings.Value;
        _appTokenSettingsSettings = appTokenSettingsSettings.Value;
        _aspNetUser = aspNetUser;
        _context = context;
    }

    public async Task<LoginResponseViewModel> GerarJwt(string email)
    {
        //Obtêm um usuário com base no email
        var user = await UserManager.FindByEmailAsync(email);

        //Obtêm as claims(Permissões) do usuário
        var claims = await UserManager.GetClaimsAsync(user);

        //Configuração de nova claism com base nas informações do tokem.
        var identityClaims = await ObterClaimsUsuario(user, claims);

        var encodedToken = CodificarToken(identityClaims);
        return ObterRespostaToken(encodedToken, user, claims);
    }

    private async Task<ClaimsIdentity> ObterClaimsUsuario(IdentityUser user, ICollection<Claim> claims)
    {
        //Obtêm as roles(Papel) do usuário
        var userRoles = await UserManager.GetRolesAsync(user);

        //Id do usuário
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));

        //Email do usuário
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

        //Cria uma chave única para o token.
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        //Configura quando o token vai expirar.
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));

        //Configura a hora que o token foi emitido
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        //Adiciona uma claim com as roles
        foreach (var userRole in userRoles)
            claims.Add(new Claim("role", userRole));

        //Configura as claim no Identity
        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);
        return identityClaims;
    }

    private string CodificarToken(ClaimsIdentity identityClaims)
    {
        //Cria o objeto para geração do Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        //Configura e Cria o Token com base nas informações configuradas pelo sistema.
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emissor,
            Audience = _appSettings.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });
        //Escreve o token e retorna.
        var encodedToken = tokenHandler.WriteToken(token);
        return encodedToken;
    }

    private LoginResponseViewModel ObterRespostaToken(string encodedToken, IdentityUser user,
        IEnumerable<Claim> claims)
    {
        return new LoginResponseViewModel
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
            UserToken = new UserTokenViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
            }
        };
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);

}
