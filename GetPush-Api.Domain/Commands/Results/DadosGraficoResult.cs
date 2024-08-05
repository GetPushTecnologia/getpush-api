namespace GetPush_Api.Domain.Commands.Results
{
    public class DadosGraficoResult
    {
        public DadosGraficoResult()
        {
            usuario = new UsuarioResult();
        }

        public UsuarioResult usuario { get; set; }
        public decimal totalContaPaga { get; set; }
        public decimal totalValorRecebido { get; set;}
    }
}
