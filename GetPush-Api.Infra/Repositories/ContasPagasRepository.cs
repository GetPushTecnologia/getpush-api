using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Infra.Repositories
{
    public class ContasPagasRepository : IContasPagasRepository
    {
        public Task<IEnumerable<ContasPagarResult>> GetContasPagas()
        {
            throw new NotImplementedException();
        }
    }
}
