
namespace OdontoApp.Models.ClassesRelacionais
{
    public class ReceitaMedicamento
    {
        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }
        public int MedicamentoId  { get; set; }
        public Medicamento Medicamento  { get; set; }
    }
}
