using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Caixa
    {
        public int CaixaId { get; set; }
        [Display(Name = "Caixa")]
        [Required(ErrorMessage = "Infome o campo {0}", AllowEmptyStrings = false)]
        public string DescricaoCaixa { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public List<Despesa> Despesas { get; set; }
    }
}
