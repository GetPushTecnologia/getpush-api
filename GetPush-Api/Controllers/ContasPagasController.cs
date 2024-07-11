using GetPush_Api.Domain.Commands.Handlers;
using GetPush_Api.Domain.Entities;
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

        private Guid UsuarioId()
        {
            var userIdString = this.User.FindFirst("usuarioId")?.Value ?? throw new InvalidOperationException("O usuário não possui um ID válido.");
            return new Guid(userIdString);            
        }

        [HttpGet]
        [Route("getContasPagas")]
        [Authorize]
        public async Task<IActionResult> GetContasPagas()
        {
            try
            {
                return Ok(await _handler.GetContasPagas(new Usuario { id = UsuarioId() }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro", Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("insertContasPagas")]
        [Authorize]
        public async Task<IActionResult> InsertContasPagas()
        {
            try
            {
                return Ok(await _handler.InsertContasPagas(new Usuario { id = UsuarioId() }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro", Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("updateContasPagas")]
        [Authorize]
        public async Task<IActionResult> UpdateContasPagas()
        {
            try
            {
                return Ok(await _handler.UpdateContasPagas(new Usuario { id = UsuarioId() }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro", Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("deleteContasPagas")]
        [Authorize]
        public async Task<IActionResult> DeleteContasPagas()
        {
            try
            {
                return Ok(await _handler.DeleteContasPagas(new Usuario { id = UsuarioId() }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro", Error = ex.Message });
            }
        }
    }
}
