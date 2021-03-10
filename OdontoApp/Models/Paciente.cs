using OdontoApp.Libraries.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        [Display(Name = "Nome do Paciente")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Informe o campo {0}", AllowEmptyStrings = false)]
        [MinLength (3, ErrorMessage = "{0} muito curto")]
        public string NomePaciente { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Nascimento { get; set; }

        [CPF(ErrorMessage = "Informe um {0} válido")]
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [StringLength(14, ErrorMessage = "Infome o {0} correto")]
        public string CPF { get; set; }

        [Display(Name = "RG")]
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        [StringLength(14, ErrorMessage = "Infome o {0} correto")]
        public string RgPaciente { get; set; }

        [Display(Name = "Observação")]
        [DataType(DataType.Text)]
        public string ObsPaciente { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        public string EmailPaciente { get; set; }

        [Display(Name = "Como chegou")]
        [Required(ErrorMessage = "Informe o campo {0}")]
        public string ComoChegouPaciente { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [MinLength(2, ErrorMessage = "Infome o {0} correto")]
        [MaxLength(2, ErrorMessage = "Infome o {0} correto")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:(##)}")]
        public string DDD { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [MinLength(8, ErrorMessage = "Infome o {0} correto")]
        [MaxLength(9, ErrorMessage = "Infome o {0} correto")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#####-####}")]
        public string Telefone { get; set; }

        [Display(Name = "Prontuário")]
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:####-##}")]
        public string NumeroProntuarioPaciente { get; set; }

        public int? PlanoId { get; set; }
        public Plano Plano { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

        public List<Anamnese> Anamneses { get; set; }
        public List<Agenda> Agendas { get; set; }
        public List<Atestado> Atestados { get; set; }
        public List<Imagem> Imagems { get; set; }
        public List<Receituario> Receituarios { get; set; }
        public List<Recebimento> Recebimentos { get; set; }
        public List<Tratamento> Tratamentos { get; set; }

    }
}
