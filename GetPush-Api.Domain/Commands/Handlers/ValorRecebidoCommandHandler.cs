using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{    
    public class ValorRecebidoCommandHandler : IValorRecebidoCommandHandler
    {
        private readonly IValorRecebidoRepository _repository;
        private readonly ITipoValorRecebidoCommandHandler _tipoValorRecebidohandler;
        public ValorRecebidoCommandHandler(IValorRecebidoRepository repository, ITipoValorRecebidoCommandHandler tipoValorRecebidohandler)
        {
            _repository = repository;
            _tipoValorRecebidohandler = tipoValorRecebidohandler;
        }

        public async Task DeleteValorRecebido(Guid valorRecebidoId)
        {
            await _repository.DeleteValorRecebido(valorRecebidoId);
        }

        public async Task<IEnumerable<ValorRecebidoResult>> GetValorRecebido(Usuario usuario)
        {
            var tipoValorRecebidoList = await _tipoValorRecebidohandler.GetTipoValorRecebido();
            return await _repository.GetValorRecebido(usuario, tipoValorRecebidoList);
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
