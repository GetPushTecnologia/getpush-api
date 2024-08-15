namespace GetPush_Api.Domain.Commands.Results
{
    public class DadosGraficoLinhaTempoResult
    {
        public DadosGraficoLinhaTempoResult()
        {
            linhaTempoContaPaga = new List<LinhaTempoContaPaga>();
            linhaTempoValorRecebido = new List<LinhaTempoValorRecebido>();
        }

        public IEnumerable<LinhaTempoContaPaga> linhaTempoContaPaga { get; set; }
        public IEnumerable<LinhaTempoValorRecebido> linhaTempoValorRecebido { get; set; }
    }

    public class LinhaTempoContaPaga
    {
        public DateTime dataPagamento { get; set; }
        public decimal totalContaPaga { get; set; }
    }

    public class LinhaTempoValorRecebido
    {
        public DateTime dataRecebimento { get; set; }
        public decimal totalValorRecebido { get; set; }
    }
}
