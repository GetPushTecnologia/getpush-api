using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Commands.Interface
{
    public interface ITipoContaPagaCommandHandler
    {
        Task<IEnumerable<TipoContaPagaResult>> GetTipoContaPaga();
        Task InsertTipoContaPaga(TipoContaPaga tipoContaPaga);
        Task UpdateTipoContaPaga(TipoContaPaga tipoContaPaga);
        Task<string> DeleteTipoContaPaga(TipoContaPaga tipoContaPaga);        
    }
}
