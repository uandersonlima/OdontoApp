using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Models
{
    public class Clinica
    {

        public int ClinicaId { get; set; }

        [Required(ErrorMessage = "Informe o campo", AllowEmptyStrings = false)]
        [Display(Name ="Nome da Clínica")]
        public string NomeClinica { get; set; }

        [Required(ErrorMessage = "Informe o campo", AllowEmptyStrings = false)]
        [Display(Name = "CNPJ")]
        public string CnpjClinica { get; set; }


        [Required(ErrorMessage = "Informe o campo", AllowEmptyStrings = false)]
        [Display(Name =" Telefone")]
        public string TelefoneClinica { get; set; }

        [Required(ErrorMessage = "Informe o campo", AllowEmptyStrings = false)]
        [Display(Name ="Nº de Cadeiras")]
        public string QuantidaDeCadeiraClinica { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public List<ClinicaCargoClinica> ClinicaCargoClinicas { get; set; }
        public List<Atestado> Atestados { get; set; }
        public List<Receituario> Receituarios { get; set; }

    }
}
