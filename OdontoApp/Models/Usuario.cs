using OdontoApp.Libraries.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string AccessType { get; set; }
        public string AccountStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string PlanNumber { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [MinLength(4, ErrorMessage = "Nome e sobrenome muito curto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        public string Sexo { get; set; }

        [CPF(ErrorMessage = "Informe um {0} válido")]
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#########'/'##}")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [MinLength(2, ErrorMessage = "Infome o {0} correto")]
        [MaxLength(2, ErrorMessage = "Infome o {0} correto")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###}")]
        public string DDD { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [MinLength(8, ErrorMessage = "Infome o {0} correto")]
        [MaxLength(10, ErrorMessage = "Infome o {0} correto")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#########}")]
        public string Telefone { get; set; }

        [EmailUnico]
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [MinLength(6, ErrorMessage = "Senha deve conter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "Senhas diferentes")]
        [DataType(DataType.Password)]
        public string ConfirmacaoSenha { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public List<Clinica> Clinicas { get; set; }

    }
}
