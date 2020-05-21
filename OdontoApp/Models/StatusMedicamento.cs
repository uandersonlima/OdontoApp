using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OdontoApp.Models
{
    public class StatusMedicamento
    {
        public int StatusMedicamentoId { get; set; }

        [Display(Name = "Situação Medicamentos")]
        [Required(ErrorMessage = "Informe o {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public string DescricaoStatusMedicamento { get; set; }


        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Medicamento> Medicamentos { get; set; }
    }
}
