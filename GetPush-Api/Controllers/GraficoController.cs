using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GetPush_Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class GraficoController : BaseController
    {
        private IGraficoCommanddHandler _handler;

        public GraficoController(IGraficoCommanddHandler handler)
        {
            _handler = handler;
        }

        private Guid UsuarioId()
        {
            var userIdString = this.User.FindFirst("usuarioId")?.Value ?? throw new InvalidOperationException("O usuário não possui um ID válido.");
            return new Guid(userIdString);
        }

        [HttpGet]
        [Route(nameof(GetDadosGraficoResumido))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Dados Grafico", Description = "Buscar dados para atualizaro o grafico")]
        [Authorize]
        public async Task<IActionResult> GetDadosGraficoResumido()
        {
            try
            {
                var dadosGrafico = await _handler.GetDadosGraficoResumido(
                    new Usuario { id = UsuarioId() }
                    );

                var msg = dadosGrafico != null ? "Dados recuperados com sucesso" : "Não retornou dados.";

                return ApiResponse(true, msg, dadosGrafico ?? new object());
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpGet]
        [Route(nameof(GetDadosGraficoLinhaTempo))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Dados Grafico", Description = "Buscar dados para atualizaro o grafico")]
        [Authorize]
        public async Task<IActionResult> GetDadosGraficoLinhaTempo()
        {
            try
            {
                var dadosGrafico = await _handler.GetDadosGraficoLinhaTempo(
                    new Usuario { id = UsuarioId() }
                    );

                var msg = dadosGrafico != null ? "Dados recuperados com sucesso" : "Não retornou dados.";

                return ApiResponse(true, msg, dadosGrafico ?? new object());
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }
    }
}