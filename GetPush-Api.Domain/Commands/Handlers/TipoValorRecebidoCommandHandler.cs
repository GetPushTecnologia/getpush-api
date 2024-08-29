using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{    
    public class TipoValorRecebidoCommandHandler : ITipoValorRecebidoCommandHandler
    {
        private readonly ITipoValorRecebidoRepository _repository;
        private readonly IValorRecebidoRepository _repositoryValorRecebido;

        public TipoValorRecebidoCommandHandler(ITipoValorRecebidoRepository repository, IValorRecebidoRepository repositoryValorRecebido)
        {
            _repository = repository;
            _repositoryValorRecebido = repositoryValorRecebido;
        }

        public async Task<string> DeleteTipoValorRecebido(TipoValorRecebido tipoValorRecebido)
        {
            if (tipoValorRecebido != null && tipoValorRecebido.usuarioCadastro != null)
            {
                var tipoValorRecebidoList = await _repository.GetTipoValorRecebido();
                var valorRecebidoList = await _repositoryValorRecebido.GetValorRecebido(tipoValorRecebido.usuarioCadastro, tipoValorRecebidoList);

                if (!valorRecebidoList.Any(x => x.tipoValorRecebido.id == tipoValorRecebido.id))
                {
                    await _repository.DeleteTipoValorRecebido(tipoValorRecebido.id.GetValueOrDefault());
                    return string.Empty;
                }

                return "Tipo de valor não pode ser deletado. Existe valores pagos cadastrados para ele.";
            }

            return string.Empty;
        }

        public async Task<IEnumerable<TipoValorRecebidoResult>> GetTipoValorRecebido()
        {
            return await _repository.GetTipoValorRecebido();
        }

        public async Task InsertTipoValorRecebido(TipoValorRecebido tipoValorRecebido)
        {
            tipoValorRecebido.code = await GetUltimoCode() + 1;
            await _repository.InsertTipoValorRecebido(tipoValorRecebido);
        }

        public async Task UpdateTipoContaPaga(TipoValorRecebido tipoValorRecebido)
        {
            await _repository.UpdateTipoContaPaga(tipoValorRecebido);
        }

        private async Task<int> GetUltimoCode()
        {
            var tipoValorRecebidoList = await _repository.GetTipoValorRecebido();

            if (tipoValorRecebidoList == null)
                return 0;

            return tipoValorRecebidoList.Max(t => t.code);
        }
    }
}
