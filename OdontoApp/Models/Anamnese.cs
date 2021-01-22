using OdontoApp.Models.ClassesRelacionais;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Anamnese
    {
        public int AnamneseId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Anamnese")]
        [DataType(DataType.Text)]
        public string DescricaoAnamnese { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        
        public int? PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int? MedicoId { get; set; }
        public Medico Medico { get; set; }
        public List<AnamnesesPerguntas> AnamnesesPerguntas { get; set; }
    }
}
