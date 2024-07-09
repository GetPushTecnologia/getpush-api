using GetPush_Api.Domain.Commands.Results;

namespace GetPush_Api.Domain.Repositories
{
    public interface IContasPagasRepository
    {
        Task<IEnumerable<ContasPagarResult>> GetContasPagas();
    }
}
