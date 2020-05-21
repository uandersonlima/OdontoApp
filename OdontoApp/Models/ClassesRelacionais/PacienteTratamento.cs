
namespace OdontoApp.Models.ClassesRelacionais
{
    public class PacienteTratamento
    {
        public int TratamentoId { get; set; }
        public Tratamento Tratamento { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
    }
}
