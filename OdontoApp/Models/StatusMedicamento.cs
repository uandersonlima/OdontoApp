using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class StatusMedicamento
    {
        public int StatusMedicamentoId { get; set; }

        [Display(Name = "Situação Medicamentos")]
        [Required(ErrorMessage = "Informe o {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public string DescricaoStatusMedicamento { get; set; }


        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public List<Medicamento> Medicamentos { get; set; }
    }
}
