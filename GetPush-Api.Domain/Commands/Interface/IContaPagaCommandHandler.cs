using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Commands.Interface
{
    public interface IContaPagaCommandHandler
    {
        Task DeleteContaPaga(Guid contaPagaId);
        Task<IEnumerable<ContaPagaResult>> GetContaPaga(Usuario usuario);
        Task InsertContaPaga(ContaPaga contasPagas);
        Task UpdateContaPaga(ContaPaga contasPagas);
        Task<IEnumerable<TotalContaPagaDiaResult>> GetContaPagaTotalDia(Usuario usuario);
    }
}
