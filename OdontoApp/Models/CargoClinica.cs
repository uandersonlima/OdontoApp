using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class CargoClinica
    {
        public int CargoClinicaId { get; set; }
        [Required(ErrorMessage = "Infome o campo {0}", AllowEmptyStrings = false)]
        [Display(Name = "Cargo Clínica")]
        public string DescricaoCargoClinica { get; set; }
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public List<ClinicaCargoClinica> ClinicaCargoClinicas { get; set; }
    }
}
