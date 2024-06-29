using GetPush_Api.Shared.Commands;

namespace GetPush_Api.Domain.Commands.Inputs
{
    public class AuthenticateUserCommand : ICommand
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
