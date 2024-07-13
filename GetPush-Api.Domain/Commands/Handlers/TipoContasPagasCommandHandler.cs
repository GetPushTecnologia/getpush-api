using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class TipoContasPagasCommandHandler
    {
        private readonly ITipoContasPagasRepository _repository;

        public TipoContasPagasCommandHandler(ITipoContasPagasRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TipoContaPagaResult>> GetTipoContaPaga()
        {
            return await _repository.GetTipoContaPaga();
        }

        public async Task InsertTipoContaPaga(TipoContaPaga tipoContaPaga)
        {
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

        private Task<int> GetUltimoCode()
        { }
    }
}
