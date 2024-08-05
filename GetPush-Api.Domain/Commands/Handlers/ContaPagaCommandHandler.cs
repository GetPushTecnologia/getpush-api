using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class ContaPagaCommandHandler : IContaPagaCommandHandler
    {
        private readonly IContaPagaRepository _repository;
        private readonly ITipoContaPagaCommandHandler _tipocontasPagashander;
        public ContaPagaCommandHandler(IContaPagaRepository repository, ITipoContaPagaCommandHandler tipocontasPagashander)
        {
            _repository = repository;
            _tipocontasPagashander = tipocontasPagashander;
        }

        public async Task DeleteContaPaga(Guid contaPagaId)
        {
            await _repository.DeleteContaPaga(contaPagaId);
        }

        public async Task<IEnumerable<ContaPagaResult>> GetContaPaga(Usuario usuario)
        {
            var tipoContaPagaList = await _tipocontasPagashander.GetTipoContaPaga();
            return await _repository.GetContaPaga(usuario, tipoContaPagaList);
        }

        public async Task InsertContaPaga(ContaPaga contasPagas)
        {
            await _repository.InsertContaPaga(contasPagas);
        }

        public async Task UpdateContaPaga(ContaPaga contasPagas)
        {
            await _repository.UpdateContaPaga(contasPagas);
        }

        public async Task<IEnumerable<TotalContaPagaDiaResult>> GetContaPagaTotalDia(Usuario usuario)
        {
            return await _repository.GetContaPagaTotalDia(usuario);
        }
    }
}
