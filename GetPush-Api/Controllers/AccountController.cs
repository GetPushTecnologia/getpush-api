using FluentValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GetPush_Api.Domain.Commands.Inputs;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using GetPush_Api.Shared;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Commands.Interface;


namespace GetPush_Api.Controllers
{
    public class AccountController : ControllerBase
    {
        private UsuarioLoginResult _usuarioResult = new UsuarioLoginResult();
        private readonly IAccountCommandHandler _handler;
        public AccountController(IAccountCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("v1/auth/sing-in")]
        public async Task<IActionResult> SingIn([FromBody] AuthenticateUserCommand command)
        {
            var _tokenOptions = new TokenOptions();

            try
            {
                if (command == null)
                    return await Response(null, new List<Notification> { new Notification("Usuario", "Usuário ou senha inválidos") });

                var identity = await GetClaims(command);

                if (identity == null)
                    return await Response(null, new List<Notification> { new Notification("Usuario", "Usuário ou senha inválidos") });

                var claims = new List<Claim>
                {
                    new Claim("nome", _usuarioResult.usuario.nome),
                    new Claim("usuarioId", _usuarioResult.usuario.id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                };

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Runtime.KeySecurityToken));

                // Configurar as credenciais do token
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Configurar o token JWT com as claims e outros parâmetros necessários
                var token = new JwtSecurityToken(
                    issuer: "seu-issuer",
                    audience: "seu-audience",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

                // Adicionar cookie de autenticação
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Pode ser configurado conforme necessidade
                    ExpiresUtc = DateTime.UtcNow.AddDays(1) // Tempo de expiração do cookie
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);

                return Ok(new
                {
                    token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro", Error = ex.Message });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("v1/auth/sign-out")]
        public IActionResult SignOut()
        {
            return Ok();
        }

        public class TokenOptions
        {
            public string Jti { get; set; }

            // Método exemplo para JtiGenerator
            public async Task<string> JtiGenerator()
            {
                // Implemente a lógica para gerar o Jti aqui
                return await Task.FromResult(Guid.NewGuid().ToString());
            }

            public DateTime IssuedAt { get; set; }
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var timeSpan = date.ToUniversalTime() - epoch;
            return (long)timeSpan.TotalSeconds;
        }

        private async Task<ClaimsIdentity> GetClaims(AuthenticateUserCommand command)
        {
            //var employee = _repository.GetByUsername(command.Email);
            var usuarioLogin = await _handler.GetUsuarioLogin(command.email);
            
            if (usuarioLogin == null)
                return await Task.FromResult<ClaimsIdentity>(null);

            _usuarioResult = usuarioLogin;

            if (!usuarioLogin.Authenticate(command.email, command.password))
            {
                if (usuarioLogin.password != command.password)
                    return await Task.FromResult<ClaimsIdentity>(null);
            }
          
            var clientManagers = Runtime.ClientManagers.ToLower().Split(',');

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioLogin.login),
                new Claim(ClaimTypes.Name, usuarioLogin.usuario.nome),
                new Claim(ClaimTypes.Email, usuarioLogin.usuario.email)
            };

            return await Task.FromResult(
                new ClaimsIdentity(
                    new GenericIdentity(usuarioLogin.usuario.nome, "Token"), claims));
        }

        private async Task<IActionResult> Response(object result, List<Notification> notifications)
        {
            if (notifications == null || notifications.Count == 0)
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = notifications.Select(n => new { n.Property, n.Message })
            });
        }
    }
}
