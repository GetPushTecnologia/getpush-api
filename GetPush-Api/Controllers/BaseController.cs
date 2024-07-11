
using FluentValidator;
using Microsoft.AspNetCore.Mvc;
using GetPush_Api.Domain.Services;
using GetPush_Api.Infra;
using GetPush_Api.Controllers.Response;


namespace GetPush_Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult ApiResponse(bool success, string message)
        {
            return Ok(new ApiResponse(success, message));
        }

        protected IActionResult ApiResponse(bool success, string message, object data)
        {
            return Ok(new { success, message, data });
        }

        protected IActionResult ErrorResponse(string message)
        {
            return StatusCode(500, new ApiResponse(false, message));
        }

        //private readonly IUow _uow;
        //private readonly ILogService _logservice;

        //public BaseController(IUow uow, ILogService logService)
        //{
        //    _uow = uow;
        //    _logservice = logService;
        //}

        //public async Task<IActionResult> Response(object result, IEnumerable<Notification> notifications)
        //{
        //    if (!notifications.Any())
        //    {
        //        try
        //        {
        //            _uow.Commit();
        //            return Ok(new
        //            {
        //                sucess = true,
        //                data = result
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            _logservice.ErrorException($"Falha Interna. Detalhes: {ex.Message}", ex);
        //            return BadRequest(new
        //            {
        //                sucess = false,
        //                erros = new[] { "Ocorreu uma falha interna no servidor." }
        //            });
        //        }
        //    }
        //    else
        //    {
        //        return Ok(new
        //        {
        //            sucess = false,
        //            notifications
        //        });
        //    }
        //}
    }
}
