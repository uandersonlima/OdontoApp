namespace OdontoApp.Models
{
    public class Imagem
    {
        public int ImagemId { get; set; }
        public byte[] ImgImagem { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
