using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Posologia
    {
        public int PosologiaId { get; set; }

        [Display(Name = "Posologia")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Informe o Campo {0}", AllowEmptyStrings = false)]
        public string DescricaoPosologia { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public List<Medicamento> Medicamentos { get; set; }
    }
}
