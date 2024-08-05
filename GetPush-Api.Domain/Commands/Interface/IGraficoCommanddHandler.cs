using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;

namespace GetPush_Api.Domain.Commands.Interface
{
    public interface IGraficoCommanddHandler
    {
        Task<DadosGraficoResult> GetDadosGrafico(Usuario usuario);
    }
}
