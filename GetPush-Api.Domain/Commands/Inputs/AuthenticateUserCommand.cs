using GetPush_Api.Shared.Commands;

namespace GetPush_Api.Domain.Commands.Inputs
{
    public class AuthenticateUserCommand : ICommand
    {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
