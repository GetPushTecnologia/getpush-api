using System;

namespace GetPush_Api.Domain.Commands.Results
{
    public class ContasPagarResult
    {
        public Guid id { get; set; }
        public string descricao { get; set; }
        public TipoContasPagar tipoContasPagar { get; set; }
        public DateTime data_pagamento { get; set; }
        public decimal valor { get; set; }
        public UsuarioResult usuario { get; set; }
        public DateTime data_cadastro { get; set; }
        public DateTime data_alterado { get; set; }
        public UsuarioResult usuarioCadastro { get; set; }
    }
}
