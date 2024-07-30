using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class ContasPagasCommandHandler : IContasPagasCommandHandler
    {
        private readonly IContasPagasRepository _repository;
        private readonly ITipoContasPagasCommandHandler _tipocontasPagashander;
        public ContasPagasCommandHandler(IContasPagasRepository repository, ITipoContasPagasCommandHandler tipocontasPagashander)
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
            return await _repository.GetContasPagas(usuario, tipoContaPagaList);
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
