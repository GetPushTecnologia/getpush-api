﻿using GetPush_Api.Domain.Commands.Handlers;
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
    public class ContasPagasController : BaseController
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
        [Route(nameof(GetContasPagas))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a pagar", Description = "Buscar lista de contas pagas")]
        [Authorize]
        public async Task<IActionResult> GetContasPagas()
        {
            try
            {
                var contasPagas = await _handler.GetContasPagas(
                    new Usuario { id = UsuarioId() }
                    );

                return ApiResponse(true, "Dados recuperados com sucesso", contasPagas);
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        [Route(nameof(InsertContasPagas))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a pagar", Description = "Inserir conta paga")]
        [Authorize]
        public async Task<IActionResult> InsertContasPagas([FromBody] ContasPagas contasPagas)
        {
            try
            {
                var usuarioId = UsuarioId();
                contasPagas.usuario = new Usuario { id = usuarioId };
                contasPagas.usuarioCadastro = new Usuario { id = usuarioId };
                contasPagas.AtualizaDataBrasil(new Utilidades());

                await _handler.InsertContasPagas(contasPagas);

                return ApiResponse(true, "Gravação realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpPut]
        [Route(nameof(UpdateContasPagas))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a pagar", Description = "Atualizar conta paga")]
        [Authorize]
        public async Task<IActionResult> UpdateContasPagas([FromBody] ContasPagas contasPagas)
        {
            try
            {
                var usuarioId = UsuarioId();
                contasPagas.usuarioCadastro = new Usuario { id = usuarioId };
                contasPagas.AtualizaDataBrasil(new Utilidades());

                await _handler.UpdateContasPagas(contasPagas);

                return ApiResponse(true, "Atualização realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route(nameof(DeleteContasPagas) + "/{contasPagasId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Contas a pagar", Description = "Exclusão conta paga")]
        [Authorize]
        public async Task<IActionResult> DeleteContasPagas(Guid contasPagasId)
        {
            try
            {   
                await _handler.DeleteContasPagas(contasPagasId);

                return ApiResponse(true, "Exclusão realizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }
    }
}
