using OdontoApp.Models.ClassesRelacionais;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Medico
    {
        public int MedicoId { get; set; }
        [Display(Name = "Médico")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Informe o {0}", AllowEmptyStrings = false)]
        public string  NomeMedico { get; set; }

        [Display(Name = "CRO")]
        [Required(ErrorMessage = "Informe o {0}", AllowEmptyStrings = false)]
        public string  NumeroCroMedico { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }




        public List<Agenda> Agendas { get; set; } 
        public List<PerguntaAnamnese> PerguntaAnamneses { get; set; } 
        public List<Atestado> Atestados { get; set; }
        public List<Imagem> Imagens { get; set; } 
        public List<Receituario> Receituarios { get; set; } 
        public List<Orcamento> Orcamentos { get; set; }
        public List<Recebimento> Recebimentos { get; set; }
        public List<ReceitaMedico> ReceitaMedicos { get; set; }

    }
}
