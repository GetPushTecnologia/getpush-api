namespace GetPush_Api.Domain.Commands.Results
{
    public class TipoContasPagar
    {
        public Guid id { get; set; }
        public int code { get; set; }
        public string descricao { get; set; } = string.Empty;
        public DateTime data_cadastro { get; set; }
        public DateTime data_alterado { get; set; }
        public UsuarioResult usuarioCadastro { get; set; } = new UsuarioResult();
    }
}
