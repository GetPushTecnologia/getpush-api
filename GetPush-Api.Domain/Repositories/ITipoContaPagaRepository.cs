using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Repositories
{
    public interface ITipoContaPagaRepository
    {
        Task<IEnumerable<TipoContaPagaResult>> GetTipoContaPaga();
        Task InsertTipoContaPaga(TipoContaPaga contasPaga);
        Task UpdateTipoContaPaga(TipoContaPaga contasPaga);
        Task DeleteTipoContaPaga(Guid tipoContaPagaId);
    }
}
