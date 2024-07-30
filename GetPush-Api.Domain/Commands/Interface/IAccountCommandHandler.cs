using GetPush_Api.Domain.Commands.Results;

namespace GetPush_Api.Domain.Commands.Interface
{
    public interface IAccountCommandHandler
    {
        Task<UsuarioLoginResult> GetUsuarioLogin(string email);
    }
}
