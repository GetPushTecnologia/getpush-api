using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Repositories
{
    public interface IContasPagasRepository
    {
        Task<IEnumerable<ContasPagarResult>> GetContasPagas(Usuario usuario);
        Task InsertContasPagas(Usuario usuario);
        Task UpdateContasPagas(Usuario usuario);
        Task DeleteContasPagas(Usuario usuario);
    }
}
