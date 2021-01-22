using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Imagem
    {
        public Guid ImagemId { get; set; } = Guid.NewGuid();
        public string Diretorio { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}
