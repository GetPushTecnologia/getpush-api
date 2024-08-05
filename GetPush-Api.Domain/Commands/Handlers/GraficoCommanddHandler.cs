using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class GraficoCommanddHandler : IGraficoCommanddHandler
    {
        private readonly IGraficoRepository _repository;
        public GraficoCommanddHandler(IGraficoRepository repository)
        {
            _repository = repository;
        }

        public Task<DadosGraficoResult> GetDadosGrafico(Usuario usuario)
        {
            return _repository.GetDadosGrafico(usuario);
        }
    }
}
