using System;
using System.Text;

namespace GetPush_Api.Domain.Commands.Results
{
    public class UsuarioLoginResult
    {
        public Guid id {  get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public DateTime data_cadastro {  get; set; }
        public DateTime data_alterado { get; set; } 
        public UsuarioResult usuarioCadastro { get; set; }
        public UsuarioResult usuario { get; set; }

        public bool Authenticate(string login, string password)
        {
            if (this.login == login && (this.password == EncryptPassword(password)))
                return true;

            return false;
        }

        private string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            var saltPassword = (password += "|d585cd61-0ebb-4860-87ae-4db9ab322b21");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(saltPassword));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}
