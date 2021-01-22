using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Atestado
    {
        public int AtestadoId { get; set; }
        [Required(ErrorMessage = "Infome o campo {0}", AllowEmptyStrings = false)]
        [Display(Name = "Atestado")]
        [DataType(DataType.Text)]
        public string DescricaoAtestado { get; set; }

        [Required(ErrorMessage = "Infome o campo {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime DataAtestado { get; set; }

        [Required(ErrorMessage = "Infome o campo {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [Display(Name = "NMas")]
        public string NMasAtestado { get; set; }

        [Required(ErrorMessage = "Infome o campo {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [Display(Name = "Observações")]
        public string ObsAtestado { get; set; }

        [Required(ErrorMessage = "Infome o campo {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [Display(Name = "Cidade Atestado")]
        public string CidAtestado { get; set; }
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public int ClinicaId { get; set; }
        public Clinica Clinica { get; set; }

    }
}
