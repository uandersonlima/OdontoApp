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

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
