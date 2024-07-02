using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class AccountCommandHandler
    {
        private readonly IAccountRepository _repository;

        public AccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<UsuarioLoginResult> GetUsuarioLogin(string email)
        {
            return await _repository.GetUsuarioLogin(email);
        }
    }
}
