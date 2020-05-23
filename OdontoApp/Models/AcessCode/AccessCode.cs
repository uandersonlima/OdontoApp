using OdontoApp.Libraries.Lang;
using System;
using System.ComponentModel.DataAnnotations;

namespace OdontoApp.Models.AccessCode
{
    public class AccessCode
    {
        [Key]
        public int Codigo { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name ="Código de Verificação")]
        public string Key { get; set; }
        public string CodeType { get; set; }
        public DateTime DataGerada { get; set; }
    }
}
