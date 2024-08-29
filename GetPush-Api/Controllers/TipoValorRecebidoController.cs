using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GetPush_Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TipoValorRecebidoController : BaseController
    {
        private readonly ITipoValorRecebidoCommandHandler _handler;

        public TipoValorRecebidoController(ITipoValorRecebidoCommandHandler handler)
        {
            _handler = handler;
        }

        private Guid UsuarioId()
        {
            var userIdString = this.User.FindFirst("usuarioId")?.Value ?? throw new InvalidOperationException("O usuário não possui um ID válido.");
            return new Guid(userIdString);
        }

        [HttpGet]
        [Route(nameof(GetTipoValorRecebido))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Tipo Valor Recebido", Description = "Buscar lista de tipo de valor recebido")]
        [Authorize]
        public async Task<IActionResult> GetTipoValorRecebido()
        {
            try
            {
                var tipoValorRecebido = await _handler.GetTipoValorRecebido();

                var msg = tipoValorRecebido.Any() ? "Dados recuperados com sucesso" : "Não retornou dados.";

                return ApiResponse(true, msg, tipoValorRecebido);
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        [Route(nameof(InsertTipoValorRecebido))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Tipo Valor Recebido", Description = "Inserir tipo valor recebido")]
        [Authorize]
        public async Task<IActionResult> InsertTipoValorRecebido([FromBody] TipoValorRecebido tipoValorRecebido)
        {
            try
            {   
                tipoValorRecebido.AtualizaDataBrasil(new Utilidades(), UsuarioId());

                await _handler.InsertTipoValorRecebido(tipoValorRecebido);

                return ApiResponse(true, "Gravação realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpPut]
        [Route(nameof(UpdateTipoValorRecebido))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Tipo Valor Recebido", Description = "Atualiza tipo valor recebido")]
        [Authorize]
        public async Task<IActionResult> UpdateTipoValorRecebido([FromBody] TipoValorRecebido tipoValorRecebido)
        {
            try
            {
                tipoValorRecebido.AtualizaDataBrasil(new Utilidades(), UsuarioId());

                await _handler.UpdateTipoContaPaga(tipoValorRecebido);

                return ApiResponse(true, "Atualização realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route(nameof(DeleteTipoValorRecebido) + "/{tipoValorRecebidoId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Tipo Valor Recebido", Description = "Exclusão tipo valor recebido")]
        [Authorize]
        public async Task<IActionResult> DeleteTipoValorRecebido(Guid tipoValorRecebidoId)
        {
            try
            {
                var tipoValorRecebido = new TipoValorRecebido { id = tipoValorRecebidoId };

                tipoValorRecebido.AtualizaDataBrasil(new Utilidades(), UsuarioId());

                var result = await _handler.DeleteTipoValorRecebido(tipoValorRecebido);

                if (!string.IsNullOrEmpty(result))
                    return ErrorResponse($"Erro: {result}");

                return ApiResponse(true, "Exclusão realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }
    }
}
