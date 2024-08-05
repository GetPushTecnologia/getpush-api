using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Infra.Repositories
{
    public class GraficoRepository : IGraficoRepository
    {
        public Task<DadosGraficoResult> GetDadosGrafico(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
