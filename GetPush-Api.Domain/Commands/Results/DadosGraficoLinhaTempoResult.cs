namespace GetPush_Api.Domain.Commands.Results
{
    public class DadosGraficoLinhaTempoResult
    {
        public DadosGraficoLinhaTempoResult()
        {
            usuario = new UsuarioResult();
            contaPaga = new List<ContaPagaResult>();
            valorRecebido  = new List<ValorRecebidoResult>();
        }

        public UsuarioResult usuario { get; set; }
        public IEnumerable<ContaPagaResult> contaPaga { get; set; }
        public IEnumerable<ValorRecebidoResult> valorRecebido { get; set; }
    }
}
