namespace OdontoApp.Models.ClassesRelacionais
{
    public class OrcamentoTratamento
    {
        public int TratamentoId { get; set; }
        public Tratamento Tratamento { get; set; }
        public int OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; }
    }
}
