using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Repositories
{
    public interface IContaPagaRepository
    {
        Task<IEnumerable<ContaPagaResult>> GetContaPaga(Usuario usuario, IEnumerable<TipoContaPagaResult> tipoContaPagaList);
        Task InsertContaPaga(ContaPaga contasPagas);
        Task UpdateContaPaga(ContaPaga contasPagas);
        Task DeleteContaPaga(Guid contasPagasId);
        Task<IEnumerable<TotalContaPagaDiaResult>> GetContaPagaTotalDia(Usuario usuario);
    }
}
