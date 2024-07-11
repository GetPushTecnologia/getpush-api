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

        public async Task DeleteContasPagas(Usuario usuario)
        {
            await _repository.DeleteContasPagas(usuario);
        }

        public async Task<IEnumerable<ContasPagarResult>> GetContasPagas(Usuario usuario)
        {
            return await _repository.GetContasPagas(usuario);
        }

        public async Task InsertContasPagas(Usuario usuario)
        {
            await _repository.DeleteContasPagas(usuario);
        }

        public async Task UpdateContasPagas(Usuario usuario)
        {
            await _repository.DeleteContasPagas(usuario);
        }
    }
}
