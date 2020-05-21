using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdontoApp.Models
{
    public class Caixa
    {
        public int CaixaId { get; set; }
        [Display(Name = "Caixa")]
        [Required(ErrorMessage = "Infome o campo {0}", AllowEmptyStrings = false)]
        public string DescricaoCaixa { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Despesa> Despesas { get; set; }
    }
}
