using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Commands.Interface
{
    public interface ITipoValorRecebidoCommandHandler
    {
        Task<string> DeleteTipoValorRecebido(TipoValorRecebido tipoValorRecebido);
        Task<IEnumerable<TipoValorRecebidoResult>> GetTipoValorRecebido();
        Task InsertTipoValorRecebido(TipoValorRecebido tipoValorRecebido);
        Task UpdateTipoContaPaga(TipoValorRecebido tipoValorRecebido);
    }
}
