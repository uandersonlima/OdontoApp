using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Agenda
    {
        public int AgendaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Inicio { get; set; }
        public Nullable<DateTime> Fim { get; set; }
        public string Situacao { get; set; }
        public bool Realizado { get; set; }
        public int? PacienteId { get; set; }
        public Paciente Paciente { get; set; }


        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        
        public int? MedicoId { get; set; }
        public Medico Medico { get; set; }
    }
}
