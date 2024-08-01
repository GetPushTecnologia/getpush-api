using GetPush_Api.Domain.Util;

namespace GetPush_Api.Domain.Entities
{
    public class ContaPaga
    {
        public ContaPaga() 
        {
            descricao = string.Empty;
            tipoContaPaga = new TipoContaPaga();
            usuario = new Usuario();
            usuarioCadastro = new Usuario();
        }       

        public Guid? id { get; set; }
        public string descricao { get; set; }
        public TipoContaPaga tipoContaPaga { get; set; } 
        public DateTime data_pagamento { get; set; }
        public decimal valor { get; set; }
        public Usuario usuario { get; set; }
        public DateTime data_cadastro { get; private set; }
        public DateTime data_alterado { get; private set; }
        public Usuario usuarioCadastro { get; set; }

        public void AtualizaDataBrasil(Utilidades utils)
        {
            data_cadastro = utils.RecuperaDataAtualBrasil();
            data_alterado = utils.RecuperaDataAtualBrasil();
        }
    }
}
