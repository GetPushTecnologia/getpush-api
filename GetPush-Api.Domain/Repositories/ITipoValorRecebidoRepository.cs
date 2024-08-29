using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Repositories
{
    public interface ITipoValorRecebidoRepository
    {
        Task DeleteTipoValorRecebido(Guid tipoValorRecebidoId);
        Task<IEnumerable<TipoValorRecebidoResult>> GetTipoValorRecebido();
        Task InsertTipoValorRecebido(TipoValorRecebido tipoValorRecebido);
        Task UpdateTipoContaPaga(TipoValorRecebido tipoValorRecebido);
    }
}
