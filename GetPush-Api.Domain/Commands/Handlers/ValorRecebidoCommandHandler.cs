
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{    
    public class ValorRecebidoCommandHandler
    {
        private readonly IValorRecebidoRepository _repository;
        public ValorRecebidoCommandHandler(IValorRecebidoRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteValorRecebido(Guid valorRecebidoId)
        {
            await _repository.DeleteValorRecebido(valorRecebidoId);
        }

        public async Task<IEnumerable<ValorRecebidoResult>> GetValorRecebido(Usuario usuario)
        {
            return await _repository.GetValorRecebido(usuario);
        }

        public async Task InsertValorRecebido(ValorRecebido valorRecebido)
        {
            await _repository.InsertValorRecebido(valorRecebido);
        }

        public async Task UpdateValorRecebido(ValorRecebido valorRecebido)
        {
            await _repository.UpdateValorRecebido(valorRecebido);
        }
    }
}
