using System;
using System.ComponentModel.DataAnnotations;
using OdontoApp.Libraries.Validation;

namespace OdontoApp.Models.DTO
{
    public class SignUpUser
    {
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
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[$*&@#])(?:([0-9a-zA-Z$*&@#])(?!\\1)){8,}$", ErrorMessage="Sua senha deve conter um dígito não alfanumérico, letras maiúsculas, minúsculas e números")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "Senhas diferentes")]
        [DataType(DataType.Password)]
        public string ConfirmacaoSenha { get; set; }

        public Endereco Endereco { get; set; }
    }
}