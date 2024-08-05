using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Repositories
{
    public interface IGraficoRepository
    {
        Task<DadosGraficoResult> GetDadosGrafico(Usuario usuario);
    }
}
