using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Repositories
{
    public interface IValorRecebidoRepository
    {
        Task DeleteValorRecebido(Guid valorRecebidoId);
        Task<IEnumerable<ValorRecebidoResult>> GetValorRecebido(Usuario usuario, IEnumerable<TipoValorRecebidoResult> tipoValorRecebidoList);
        Task InsertValorRecebido(ValorRecebido valorRecebido);
        Task UpdateValorRecebido(ValorRecebido valorRecebido);
    }
}
