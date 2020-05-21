using OdontoApp.Models.ClassesRelacionais;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdontoApp.Models
{
    public class Anamnese
    {
        public int AnamneseId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Anamnese")]
        [DataType(DataType.Text)]
        public string DescricaoAnamnese { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int? PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int? MedicoId { get; set; }
        public Medico Medico { get; set; }
        public List<AnamnesesPerguntas> AnamnesesPerguntas { get; set; }
    }
}
