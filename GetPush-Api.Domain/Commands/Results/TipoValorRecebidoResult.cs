using System;

namespace GetPush_Api.Domain.Commands.Results
{
    public class TipoValorRecebidoResult
    {
        public TipoValorRecebidoResult()
        {
            descricao = string.Empty;
            usuarioCadastro = new UsuarioResult();
        }

        public Guid? id { get; set; }
        public int code { get; set; }
        public string descricao { get; set; }
        public DateTime? data_cadastro { get; set; }
        public DateTime? data_alterado { get; set; }
        public UsuarioResult usuarioCadastro { get; set; }
    }
}
