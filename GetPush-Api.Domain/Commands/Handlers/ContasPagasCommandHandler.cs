using GetPush_Api.Domain.Commands.Results;
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

        public async Task<IEnumerable<ContasPagarResult>> GetContasPagas()
        {
            return await _repository.GetContasPagas();
        }
    }
}
