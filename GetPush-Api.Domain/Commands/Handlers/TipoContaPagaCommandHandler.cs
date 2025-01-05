using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class TipoContaPagaCommandHandler : ITipoContaPagaCommandHandler
    {
        private readonly ITipoContaPagaRepository _repository;
        private readonly IContaPagaRepository _repositoryContaPaga;

        public TipoContaPagaCommandHandler(ITipoContaPagaRepository repository, IContaPagaRepository repositoryContaPaga)
        {
            _repository = repository;
            _repositoryContaPaga = repositoryContaPaga;
        }

        public async Task<IEnumerable<TipoContaPagaResult>> GetTipoContaPaga()
        {
            return await _repository.GetTipoContaPaga();
        }

        public async Task InsertTipoContaPaga(TipoContaPaga tipoContaPaga)
        {
            tipoContaPaga.code = await GetUltimoCode() + 1;
            await _repository.InsertTipoContaPaga(tipoContaPaga);
        }

        public async Task UpdateTipoContaPaga(TipoContaPaga tipoContaPaga)
        {
            await _repository.UpdateTipoContaPaga(tipoContaPaga);
        }

        public async Task<string> DeleteTipoContaPaga(TipoContaPaga tipoContaPaga)
        {
            if (tipoContaPaga != null && tipoContaPaga.usuarioCadastro != null)
            {
                var tipoContaPagaList = await _repository.GetTipoContaPaga();
                var contaPagaList = await _repositoryContaPaga.GetContaPaga(tipoContaPaga.usuarioCadastro, tipoContaPagaList);

                if (!contaPagaList.Any(x => x.tipoContaPaga.id == tipoContaPaga.id))
                {
                    await _repository.DeleteTipoContaPaga(tipoContaPaga.id.GetValueOrDefault());
                    return string.Empty;
                }

                return "Tipo conta não pode ser deletado. Existe Contas pagas cadastradas para ele";
            }

            return string.Empty;
        }

        private async Task<int> GetUltimoCode()
        {
            var tipoContaPagaList = await _repository.GetTipoContaPaga();

            if (tipoContaPagaList == null)
                return 0;

            return tipoContaPagaList.Max(t => t.code);
        }
    }
}
