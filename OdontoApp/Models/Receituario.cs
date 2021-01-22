using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Receituario
    {
        public int ReceituarioId { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }

        public int MedicoId { get; set; }
        public Medico Medico { get; set; }

        public int ClinicaId { get; set; }
        public Clinica Clinica { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

    }
}
