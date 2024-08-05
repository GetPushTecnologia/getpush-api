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
    public class ContaPagaController : BaseController
    {
        private readonly IContaPagaCommandHandler _handler;
                
        public ContaPagaController(IContaPagaCommandHandler handler)
        {
            _handler = handler;
        }

        private Guid UsuarioId()
        {
            var userIdString = this.User.FindFirst("usuarioId")?.Value ?? throw new InvalidOperationException("O usuário não possui um ID válido.");
            return new Guid(userIdString);
        }
                
        [HttpGet]
        [Route(nameof(GetContaPaga))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas pagas", Description = "Buscar lista de contas pagas")]
        [Authorize]
        public async Task<IActionResult> GetContaPaga()
        {
            try
            {
                var contasPagas = await _handler.GetContaPaga(
                    new Usuario { id = UsuarioId() }
                    );

                var msg = contasPagas.Count() > 0 ? "Dados recuperados com sucesso" : "Não retornou dados.";

                return ApiResponse(true, msg, contasPagas);
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        [Route(nameof(InsertContaPaga))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a pagar", Description = "Inserir conta paga")]
        [Authorize]
        public async Task<IActionResult> InsertContaPaga([FromBody] ContaPaga contasPagas)
        {
            try
            {
                var usuarioId = UsuarioId();
                contasPagas.usuario = new Usuario { id = usuarioId };
                contasPagas.usuarioCadastro = new Usuario { id = usuarioId };
                contasPagas.AtualizaDataBrasil(new Utilidades());

                await _handler.InsertContaPaga(contasPagas);

                return ApiResponse(true, "Gravação realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpPut]
        [Route(nameof(UpdateContaPaga))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a pagar", Description = "Atualizar conta paga")]
        [Authorize]
        public async Task<IActionResult> UpdateContaPaga([FromBody] ContaPaga contasPagas)
        {
            try
            {
                var usuarioId = UsuarioId();
                contasPagas.usuarioCadastro = new Usuario { id = usuarioId };
                contasPagas.AtualizaDataBrasil(new Utilidades());

                await _handler.UpdateContaPaga(contasPagas);

                return ApiResponse(true, "Atualização realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route(nameof(DeleteContaPaga) + "/{contaPagaId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a pagar", Description = "Exclusão conta paga")]
        [Authorize]
        public async Task<IActionResult> DeleteContaPaga(Guid contaPagaId)
        {
            try
            {   
                await _handler.DeleteContaPaga(contaPagaId);

                return ApiResponse(true, "Exclusão realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpGet]
        [Route(nameof(GetContaPagaTotalDia))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas pagas", Description = "Buscar lista de contas pagas com total por dia")]
        [Authorize]
        public async Task<IActionResult> GetContaPagaTotalDia()
        {
            try
            {
                var contasPagas = await _handler.GetContaPagaTotalDia(
                    new Usuario { id = UsuarioId() }
                    );

                var msg = contasPagas.Count() > 0 ? "Dados recuperados com sucesso" : "Não retornou dados.";

                return ApiResponse(true, msg, contasPagas);
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }
    }
}
