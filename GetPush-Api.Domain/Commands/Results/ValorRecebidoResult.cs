namespace GetPush_Api.Domain.Commands.Results
{
    public class ValorRecebidoResult
    {
        public ValorRecebidoResult()
        {
            descricao = string.Empty;
            tipoValorRecebido = new TipoValorRecebidoResult();
            usuario = new UsuarioResult();
            usuarioCadastro = new UsuarioResult();
        }

        public Guid id { get; set; } 
        public string descricao { get; set; }
        public TipoValorRecebidoResult tipoValorRecebido { get; set; }
        public decimal valor_recebido { get; set; }
        public DateTime data_recebimento { get; set; }
        public UsuarioResult usuario { get; set; }
        public DateTime data_cadastro { get; set; }
        public DateTime data_alterado { get; set; }
        public UsuarioResult usuarioCadastro { get; set; }
    }
}
