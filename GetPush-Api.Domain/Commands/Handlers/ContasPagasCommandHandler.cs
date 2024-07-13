using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class ContasPagasCommandHandler
    {
        private readonly IContasPagasRepository _repository;
        public ContasPagasCommandHandler(IContasPagasRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteContasPagas(Guid contaPagaId)
        {
            await _repository.DeleteContasPagas(contaPagaId);
        }

        public async Task<IEnumerable<ContasPagasResult>> GetContasPagas(Usuario usuario)
        {
            return await _repository.GetContasPagas(usuario);
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
