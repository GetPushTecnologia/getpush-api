using FluentValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GetPush_Api.Domain.Commands.Inputs;
using GetPush_Api.Domain.Services;
using GetPush_Api.Infra;
using System.Security.Claims;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using GetPush_Api.Shared;
using System.Security.Principal;
using GetPush_Api.Domain.Commands.Handlers;
using System.Reflection.Metadata;
using Microsoft.Extensions.Logging.Abstractions;

namespace GetPush_Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AccountCommandHandler _handler;
        public AccountController(IUow uow, ILogService logService, AccountCommandHandler handler) : base(uow, logService)
        {
            _handler = handler;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("v1/auth/sing-in")]
        public async Task<IActionResult> SingIn([FromBody] AuthenticateUserCommand command)
        {
            try
            {
                if (command == null)
                    return await Response(null, new List<Notification> { new Notification("Usuario", "Usuário ou senha inválidos") });

                var identity = await GetClaims(command);
                if (identity == null)
                    return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

                //var claims = new List<Claim>()
                //{
                //    new Claim("name", _employee.Name.ToString()),
                //    new Claim("employee", _employee.Id.ToString()),
                //    new Claim(JwtRegisteredClaimNames.UniqueName, command.Email),
                //    new Claim(JwtRegisteredClaimNames.NameId, command.Email),
                //    new Claim(JwtRegisteredClaimNames.Email, command.Email),
                //    new Claim(JwtRegisteredClaimNames.Sub, command.Email),
                //    new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                //    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                //};
                //claims.AddRange(identity.Claims);
                //var jwt = new JwtSecurityToken(
                //    issuer: _tokenOptions.Issuer,
                //audience: _tokenOptions.Audience,
                //claims: claims.AsEnumerable(),
                //    notBefore: _tokenOptions.NotBefore,
                //    expires: _tokenOptions.Expiration,
                //    signingCredentials: _tokenOptions.SigningCredentials);

                //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                //var response = new
                //{
                //    token = encodedJwt,
                //    expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                //    user = new
                //    {
                //        id = _employee.Id,
                //        name = _employee.Name.ToString(),
                //        email = _employee.Email.Address,
                //        username = _employee.User.Username,
                //        teste = "teste",
                //        requestPassword = _employee.User.requestPassword
                //    }s
                //};

                //var json = JsonConvert.SerializeOsbject(response, _serializerSettings);
                //return new OkObjectResult(json);
                
                return null;
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        private Task<ClaimsIdentity> GetClaims(AuthenticateUserCommand command)
        {
            //var employee = _repository.GetByUsername(command.Email);
            var usuarioLogin = _handler.GetUsuarioLogin(command.Email);

            //if (employee == null)
            //    return Task.FromResult<ClaimsIdentity>(null);

            //if (!employee.User.Authenticate(command.Email, command.Password))
            //{
            //    if (employee.User.Password != command.Password)
            //        return Task.FromResult<ClaimsIdentity>(null);
            //}

            //_employee = employee;

            //var clientManagers = Runtime.ClientManagers.ToLower().Split(',');

            //var claims = new List<Claim>();

            //if (employee.Type == Domain.Enums.EEmployeeType.Administrator ||
            //    employee.Type == Domain.Enums.EEmployeeType.Common ||
            //    employee.Type == Domain.Enums.EEmployeeType.Stockbroker ||
            //    employee.Type == Domain.Enums.EEmployeeType.MasterCapital)
            //{
            //    claims.Add(new Claim("GestaoReno", Enum.GetName(typeof(Domain.Enums.EEmployeeType), employee.Type)));
            //}

            //if (employee.Type == Domain.Enums.EEmployeeType.Administrator ||
            //    employee.Type == Domain.Enums.EEmployeeType.MasterProperties ||
            //    employee.Type == Domain.Enums.EEmployeeType.MasterCapital)
            //{
            //    claims.Add(new Claim("GestaoRenoJuridico", Enum.GetName(typeof(Domain.Enums.EEmployeeType), employee.Type)));
            //}

            //if (clientManagers.Contains(employee.User.Username))
            //    claims.Add(new Claim("ClientManager", Enum.GetName(typeof(Domain.Enums.EEmployeeType), employee.Type)));

            //return Task.FromResult(
            //    new ClaimsIdentity(
            //        new GenericIdentity(employee.User.Username, "Token"), claims));

            return null;
        }
    }
}
