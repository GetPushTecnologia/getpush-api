using System;

namespace GetPush_Api.Domain.Commands.Results
{
    public class UsuarioResult
    {
        public UsuarioResult()
        {
            nome = string.Empty;
            cpf = string.Empty;
            email = string.Empty;
            sexo = string.Empty;
            usuarioCadastro = null;
        }

        public Guid id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public DateTime nascimento { get; set; }
        public string sexo { get; set; }
        public bool ativo { get; set; }
        public DateTime data_cadastro { get; set; }
        public DateTime data_alterado { get; set; }
        public UsuarioResult? usuarioCadastro { get; set; }
    }
}
