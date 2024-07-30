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
    public class ValorRecebidoController : BaseController
    {
        private readonly IValorRecebidoCommandHandler _handler;

        public ValorRecebidoController(IValorRecebidoCommandHandler handler)
        {
            _handler = handler;
        }

        private Guid UsuarioId()
        {
            var userIdString = this.User.FindFirst("usuarioId")?.Value ?? throw new InvalidOperationException("O usuário não possui um ID válido.");
            return new Guid(userIdString);
        }

        [HttpGet]
        [Route(nameof(GetValorRecebido))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Valor Recebido", Description = "Buscar lista de valores recebidos")]
        [Authorize]
        public async Task<IActionResult> GetValorRecebido()
        {
            try
            {
                var valorRecebido = await _handler.GetValorRecebido(
                    new Usuario { id = UsuarioId() }
                    );

                var msg = valorRecebido.Count() > 0 ? "Dados recuperados com sucesso" : "Não retornou dados.";

                return ApiResponse(true, msg, valorRecebido);
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        [Route(nameof(InsertValorRecebido))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Valor Recebido", Description = "Inserir valor recebido")]
        [Authorize]
        public async Task<IActionResult> InsertValorRecebido([FromBody] ValorRecebido valorRecebido)
        {
            try
            {
                var usuarioId = UsuarioId();
                valorRecebido.usuarioCadastro = new Usuario { id = usuarioId };
                valorRecebido.usuario = new Usuario { id = usuarioId };
                valorRecebido.AtualizaDataBrasil(new Utilidades());

                await _handler.InsertValorRecebido(valorRecebido);

                return ApiResponse(true, "Gravação realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpPut]
        [Route(nameof(UpdateValorRecebido))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Valor Recebido", Description = "Atualiza valor recebido")]
        [Authorize]
        public async Task<IActionResult> UpdateValorRecebido([FromBody] ValorRecebido valorRecebido)
        {
            try
            {
                var usuarioId = UsuarioId();
                valorRecebido.usuarioCadastro = new Usuario { id = usuarioId };
                valorRecebido.AtualizaDataBrasil(new Utilidades());

                await _handler.UpdateValorRecebido(valorRecebido);

                return ApiResponse(true, "Atualização realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route(nameof(DeleteValorRecebido) + "/{valorRecebidoId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Valor Recebido", Description = "Exclusão valor recebido")]
        [Authorize]
        public async Task<IActionResult> DeleteValorRecebido(Guid valorRecebidoId)
        {
            try
            {
                await _handler.DeleteValorRecebido(valorRecebidoId);

                return ApiResponse(true, "Exclusão realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }
    }
}
