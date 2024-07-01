using GetPush_Api.Domain.Commands.Results;

namespace GetPush_Api.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<UsuarioLoginResult> GetUsuarioLogin(string email);
    }
}
