using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Commands.Interface
{
    public interface IContasPagasCommandHandler
    {
        Task DeleteContasPagas(Guid contaPagaId);
        Task<IEnumerable<ContasPagasResult>> GetContasPagas(Usuario usuario);
        Task InsertContasPagas(ContasPagas contasPagas);
        Task UpdateContasPagas(ContasPagas contasPagas);
    }
}
