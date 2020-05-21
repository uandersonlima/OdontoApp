using OdontoApp.Models;
using OdontoApp.Repositories.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

            IUsuarioRepository usuarioRepos = (IUsuarioRepository)validationContext.GetService(typeof(IUsuarioRepository));
            List<Usuario> usuarios = usuarioRepos.GetUserByEmail(Email);

            Usuario objCliente = (Usuario)validationContext.ObjectInstance;

            if (usuarios.Count > 1)
            {
                return new ValidationResult("E-mail já existente!");
            }
            if (usuarios.Count == 1 && objCliente.UsuarioId != usuarios.First().UsuarioId)
            {
                return new ValidationResult("E-mail já existente!");
            }


            return ValidationResult.Success;
        }
    }
}
