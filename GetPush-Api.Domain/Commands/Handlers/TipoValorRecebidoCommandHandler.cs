using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{    
    public class TipoValorRecebidoCommandHandler : ITipoValorRecebidoCommandHandler
    {
        private readonly ITipoValorRecebidoRepository _repository;

        public TipoValorRecebidoCommandHandler(ITipoValorRecebidoRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteTipoValorRecebido(Guid tipoContipoValorRecebidoIdtaPagaId)
        {
            await _repository.DeleteTipoValorRecebido(tipoContipoValorRecebidoIdtaPagaId);
        }

        public async Task<IEnumerable<TipoValorRecebidoResult>> GetTipoValorRecebido()
        {
            return await _repository.GetTipoValorRecebido();
        }

        public async Task InsertTipoValorRecebido(TipoValorRecebido tipoValorRecebido)
        {
            tipoValorRecebido.code = await GetUltimoCode();
            await _repository.InsertTipoValorRecebido(tipoValorRecebido);
        }

        public async Task UpdateTipoContaPaga(TipoValorRecebido tipoValorRecebido)
        {
            await _repository.UpdateTipoContaPaga(tipoValorRecebido);
        }

        private async Task<int> GetUltimoCode()
        {
            var tipoValorRecebidoList = await _repository.GetTipoValorRecebido();
            return tipoValorRecebidoList.Max(t => t.code);
        }
    }
}
