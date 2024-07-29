using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class ContasPagasCommandHandler
    {
        private readonly IContasPagasRepository _repository;
        private readonly TipoContasPagasCommandHandler _tipocontasPagashander;
        public ContasPagasCommandHandler(IContasPagasRepository repository, TipoContasPagasCommandHandler tipocontasPagashander)
        {
            _repository = repository;
            _tipocontasPagashander = tipocontasPagashander;
        }

        public async Task DeleteContasPagas(Guid contaPagaId)
        {
            await _repository.DeleteContasPagas(contaPagaId);
        }

        public async Task<IEnumerable<ContasPagasResult>> GetContasPagas(Usuario usuario)
        {
            var tipoContaPagaList = await _tipocontasPagashander.GetTipoContaPaga();
            return await _repository.GetContasPagas(usuario, (IEnumerable<TipoContaPagaResult>)tipoContaPagaList);
        }

        public async Task InsertContasPagas(ContasPagas contasPagas)
        {
            await _repository.InsertContasPagas(contasPagas);
        }

        public async Task UpdateContasPagas(ContasPagas contasPagas)
        {
            await _repository.UpdateContasPagas(contasPagas);
        }
    }
}
