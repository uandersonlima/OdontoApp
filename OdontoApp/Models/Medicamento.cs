using OdontoApp.Models.ClassesRelacionais;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdontoApp.Models
{
    public class Medicamento
    {
        public int MedicamentoId { get; set; }
        [Display(Name = "Medicamentos")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Informe o {0}", AllowEmptyStrings = false)]
        public string DescricaoMedicamento { get; set; }
        public int StatusMedicamentoId { get; set; }
        public StatusMedicamento StatusMedicamento { get; set; }
        public int PosologiaId { get; set; }
        public Posologia Posologia { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<ReceitaMedicamento> ReceitaMedicamentos { get; set; }
    }
}
