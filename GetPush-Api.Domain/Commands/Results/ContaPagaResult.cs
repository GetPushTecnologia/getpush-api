namespace GetPush_Api.Domain.Commands.Results
{
    public class ContaPagaResult
    {
        public ContaPagaResult()
        {
            descricao = string.Empty;
            tipoContaPaga = new TipoContaPagaResult();
            usuario = new UsuarioResult();
            usuarioCadastro = new UsuarioResult();
        }

        public Guid id { get; set; }
        public string descricao { get; set; }
        public TipoContaPagaResult tipoContaPaga { get; set; }
        public DateTime data_pagamento { get; set; }
        public decimal valor_pago { get; set; }
        public UsuarioResult usuario { get; set; }
        public DateTime data_cadastro { get; set; }
        public DateTime data_alterado { get; set; }
        public UsuarioResult usuarioCadastro { get; set; }
    }
}
