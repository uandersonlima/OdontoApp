using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OdontoApp.Models
{
    public class CargoClinica
    {
        public int CargoClinicaId { get; set; }
        [Required(ErrorMessage = "Infome o campo {0}", AllowEmptyStrings = false)]
        [Display(Name = "Cargo Clínica")]
        public string DescricaoCargoClinica { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<ClinicaCargoClinica> ClinicaCargoClinicas { get; set; }
    }
}
