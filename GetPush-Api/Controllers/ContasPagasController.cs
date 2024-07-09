using GetPush_Api.Domain.Commands.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetPush_Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ContasPagasController : ControllerBase
    {
        private readonly ContasPagasCommandHandler _handler;
        public ContasPagasController(ContasPagasCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        [Route("getContasPagas")]
        [Authorize]
        public async Task<IActionResult> GetContasPagas()
        {
            try
            {
                return Ok(_handler.GetContasPagas());
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Erro", Error = ex.Message });
            }
        }
    }
}
