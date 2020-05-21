
namespace OdontoApp.Models.ClassesRelacionais
{
    public class ReceitaMedico
    {
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }

        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }
    }
}
