using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Repositories
{
    public interface IContasPagasRepository
    {
        Task<IEnumerable<ContasPagasResult>> GetContasPagas(Usuario usuario, IEnumerable<TipoContaPagaResult> tipoContaPagaList);
        Task InsertContasPagas(ContasPagas contasPagas);
        Task UpdateContasPagas(ContasPagas contasPagas);
        Task DeleteContasPagas(Guid contasPagasId);
    }
}
