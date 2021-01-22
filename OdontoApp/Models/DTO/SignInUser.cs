using System.ComponentModel.DataAnnotations;

namespace OdontoApp.Models.DTO
{
    public class SignInUser
    {
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [MinLength(6, ErrorMessage = "Senha deve conter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}