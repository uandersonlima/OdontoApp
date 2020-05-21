using OdontoApp.Libraries.Lang;
using System;
using System.ComponentModel.DataAnnotations;

namespace OdontoApp.Models.CodigoAcesso
{
    public class CodigoAcesso
    {
        [Key]
        public int Codigo { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name ="Código de Verificação")]
        public string CodAcesso { get; set; }
        public string TipoCodigo { get; set; }
        public DateTime DataGerada { get; set; }
    }
}
