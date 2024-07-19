using GetPush_Api.Domain.Util;

namespace GetPush_Api.Domain.Entities
{
    public class ValorRecebido
    {
        public ValorRecebido()
        {
            descricao = string.Empty;
            tipoValorRecebido = new TipoValorRecebido();
            usuario = new Usuario();
            usuarioCadastro = new Usuario();
        }

        public Guid? id { get; set; }
        public string descricao { get; set; }
        public TipoValorRecebido tipoValorRecebido { get; set; }
        public decimal valor { get; set; }
        public DateTime data_recebimento { get; set; }
        public Usuario usuario { get; set; }
        public DateTime data_cadastro { get; set; }
        public DateTime data_alterado { get; set; }
        public Usuario usuarioCadastro { get; set; }

        public void AtualizaDataBrasil(Utilidades utils)
        {
            data_cadastro = utils.RecuperaDataAtualBrasil();
            data_alterado = utils.RecuperaDataAtualBrasil();
        }
    }
}
