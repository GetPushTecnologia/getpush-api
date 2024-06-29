using FluentValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GetPush_Api.Domain.Commands.Inputs;
using GetPush_Api.Domain.Services;
using GetPush_Api.Infra;
using System.Security.Claims;
using System.Security.Policy;

namespace GetPush_Api.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUow uow, ILogService logService) : base(uow, logService)
        {
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
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        private Task<ClaimsIdentity> GetClaims(AuthenticateUserCommand command)
        {
            return null;
        }
    }
}
