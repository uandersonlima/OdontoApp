using Microsoft.AspNetCore.Identity;
using OdontoApp.Libraries.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AccessType { get; set; }
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

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public List<Clinica> Clinicas { get; set; }

    }
}
