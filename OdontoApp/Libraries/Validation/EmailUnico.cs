using OdontoApp.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OdontoApp.Libraries.Validation
{
    public sealed class  EmailUnico : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Digite o e-mail!");
            }  
                      
            string Email = (value as string).Trim();
            IUsuarioService usuarioService = (IUsuarioService)validationContext.GetService(typeof(IUsuarioService));
            
            var emailAlreadyRegistered = usuarioService.ValidateEmailAsync(Email).Result;
            if (emailAlreadyRegistered)
                return new ValidationResult("E-mail já existente!");            

            return ValidationResult.Success;
        }
    }
}
