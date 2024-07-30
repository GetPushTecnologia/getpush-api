using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Commands.Interface
{
    public interface IValorRecebidoCommandHandler
    {
        Task DeleteValorRecebido(Guid valorRecebidoId);
        Task<IEnumerable<ValorRecebidoResult>> GetValorRecebido(Usuario usuario);
        Task InsertValorRecebido(ValorRecebido valorRecebido);
        Task UpdateValorRecebido(ValorRecebido valorRecebido);        
    }
}
