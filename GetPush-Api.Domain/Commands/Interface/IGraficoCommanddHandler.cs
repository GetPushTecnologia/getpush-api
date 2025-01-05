using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Commands.Interface
{
    public interface IGraficoCommanddHandler
    {
        Task<DadosGraficoResult> GetDadosGraficoResumido(Usuario usuario);
        Task<DadosGraficoLinhaTempoResult> GetDadosGraficoLinhaTempo(Usuario usuario);
    }
}
