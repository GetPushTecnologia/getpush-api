
using FluentValidator;
using Microsoft.AspNetCore.Mvc;
using GetPush_Api.Domain.Services;
using GetPush_Api.Infra;


namespace GetPush_Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUow _uow;
        private readonly ILogService _logservice;

        public BaseController(IUow uow, ILogService logService)
        {
            _uow = uow;
            _logservice = logService;
        }

        public async Task<IActionResult> Response(object result, IEnumerable<Notification> notifications)
        {
            if (!notifications.Any())
            {
                try
                {
                    _uow.Commit();
                    return Ok(new
                    {
                        sucess = true,
                        data = result
                    });
                }
                catch (Exception ex)
                {
                    _logservice.ErrorException($"Falha Interna. Detalhes: {ex.Message}", ex);
                    return BadRequest(new
                    {
                        sucess = false,
                        erros = new[] { "Ocorreu uma falha interna no servidor." }
                    });
                }
            }
            else
            {
                return Ok(new
                {
                    sucess = false,
                    notifications
                });
            }
        }
    }
}
