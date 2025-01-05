using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Repositories
{
    public interface IGraficoRepository
    {
        Task<DadosGraficoResult> GetDadosGraficoResumido(Usuario usuario);
        Task<IEnumerable<LinhaTempoContaPaga>> GetLinhaTempoContaPaga(Usuario usuario);
        Task<IEnumerable<LinhaTempoValorRecebido>> GetLinhaTempoValorRecebido(Usuario usuario);
    }
}
