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
    public class TipoContaPagaController : BaseController
    {
        private readonly ITipoContaPagaCommandHandler _handler;

        public TipoContaPagaController(ITipoContaPagaCommandHandler handler)
        {
            _handler = handler;
        }

        private Guid UsuarioId()
        {
            var userIdString = this.User.FindFirst("usuarioId")?.Value ?? throw new InvalidOperationException("O usuário não possui um ID válido.");
            return new Guid(userIdString);
        }

        [HttpGet]
        [Route(nameof(GetTipoContaPaga))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Tipo Contas pagas", Description = "Buscar lista de tipo de contas pagas")]
        [Authorize]
        public async Task<IActionResult> GetTipoContaPaga()
        {
            try
            {
                var tipoContasPagas = await _handler.GetTipoContaPaga();

                var msg = tipoContasPagas.Count() > 0 ? "Dados recuperados com sucesso" : "Não retornou dados.";

                return ApiResponse(true, msg, tipoContasPagas);
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        [Route(nameof(InsertTipoContaPaga))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Tipo Contas pagas", Description = "Inserir tipo conta paga")]
        [Authorize]
        public async Task<IActionResult> InsertTipoContaPaga([FromBody] TipoContaPaga tipoContaPaga)
        {
            try
            {
                var usuarioId = UsuarioId();
                tipoContaPaga.usuarioCadastro = new Usuario { id = usuarioId };
                tipoContaPaga.AtualizaDataBrasil(new Utilidades());

                await _handler.InsertTipoContaPaga(tipoContaPaga);

                return ApiResponse(true, "Gravação realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpPut]
        [Route(nameof(UpdateTipoContaPaga))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Tipo Contas pagas", Description = "Atualiza tipo contas paga")]
        [Authorize]
        public async Task<IActionResult> UpdateTipoContaPaga([FromBody] TipoContaPaga tipoContaPaga)
        {
            try
            {
                var usuarioId = UsuarioId();
                tipoContaPaga.usuarioCadastro = new Usuario { id = usuarioId };
                tipoContaPaga.AtualizaDataBrasil(new Utilidades());

                await _handler.UpdateTipoContaPaga(tipoContaPaga);

                return ApiResponse(true, "Atualização realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route(nameof(DeleteTipoContaPaga) + "/{tipoContaPagaId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Tipo Contas pagas", Description = "Exclusão tipo conta paga")]
        [Authorize]
        public async Task<IActionResult> DeleteTipoContaPaga(Guid tipoContaPagaId)
        {
            try
            {
                await _handler.DeleteTipoContaPaga(tipoContaPagaId);

                return ApiResponse(true, "Exclusão realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }
    }
}
