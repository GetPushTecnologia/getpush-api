using GetPush_Api.Domain.Util;

namespace GetPush_Api.Domain.Entities
{
    public class TipoContaPaga
    {
        public TipoContaPaga()
        {
            descricao = string.Empty;
            usuarioCadastro = new Usuario();
        }

        public Guid? id { get; set; }
        public int code { get; set; }
        public string descricao { get; set; }
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
