﻿using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class GraficoCommanddHandler : IGraficoCommanddHandler
    {
        private readonly IGraficoRepository _repository;
        private readonly IContaPagaCommandHandler _contaPagaHandler;
        private readonly IValorRecebidoCommandHandler _valorRecebidoHandler;

        public GraficoCommanddHandler(IGraficoRepository repository, IContaPagaCommandHandler contaPagaHandler, IValorRecebidoCommandHandler valorRecebidoHandler)
        {
            _repository = repository;
            _contaPagaHandler = contaPagaHandler;
            _valorRecebidoHandler = valorRecebidoHandler;
        }

        public Task<DadosGraficoResult> GetDadosGraficoResumido(Usuario usuario)
        {
            return _repository.GetDadosGraficoResumido(usuario);
        }

        public async Task<DadosGraficoLinhaTempoResult> GetDadosGraficoLinhaTempo(Usuario usuario)
        {
            if (usuario != null)
            {
                var contaPaga = await _contaPagaHandler.GetContaPaga(usuario);
                var valorRecebido = await _valorRecebidoHandler.GetValorRecebido(usuario);

                return new DadosGraficoLinhaTempoResult
                {
                    usuario = new UsuarioResult { id = usuario?.id ?? Guid.Empty },
                    contaPaga = contaPaga,
                    valorRecebido = valorRecebido
                };
            }

            return new DadosGraficoLinhaTempoResult();
        }
    }
}
