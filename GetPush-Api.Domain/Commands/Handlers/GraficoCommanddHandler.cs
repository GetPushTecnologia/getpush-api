using GetPush_Api.Domain.Commands.Interface;
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

        public async Task<DadosGraficoResult> GetDadosGraficoResumido(Usuario usuario)
        {
            return await _repository.GetDadosGraficoResumido(usuario);
        }

        public async Task<DadosGraficoLinhaTempoResult> GetDadosGraficoLinhaTempo(Usuario usuario)
        {
            var contaPaga = await _repository.GetLinhaTempoContaPaga(usuario);
            var valorRecebido = await _repository.GetLinhaTempoValorRecebido(usuario);

            return new DadosGraficoLinhaTempoResult
            {
                linhaTempoContaPaga = contaPaga,
                linhaTempoValorRecebido = valorRecebido
            };
        }
    }
}
