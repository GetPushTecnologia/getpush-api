using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class TipoContaPagaCommandHandler : ITipoContaPagaCommandHandler
    {
        private readonly ITipoContaPagaRepository _repository;

        public TipoContaPagaCommandHandler(ITipoContaPagaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TipoContaPagaResult>> GetTipoContaPaga()
        {
            return await _repository.GetTipoContaPaga();
        }

        public async Task InsertTipoContaPaga(TipoContaPaga tipoContaPaga)
        {
            tipoContaPaga.code = await GetUltimoCode();
            await _repository.InsertTipoContaPaga(tipoContaPaga);
        }

        public async Task UpdateTipoContaPaga(TipoContaPaga tipoContaPaga)
        {
            await _repository.UpdateTipoContaPaga(tipoContaPaga);
        }

        public async Task DeleteTipoContaPaga(Guid tipoContaPagaId)
        {
            await _repository.DeleteTipoContaPaga(tipoContaPagaId);
        }

        private async Task<int> GetUltimoCode()
        {
            var tipoContaPagaList = await _repository.GetTipoContaPaga();
            return tipoContaPagaList.Max(t => t.code);
        }
    }
}
