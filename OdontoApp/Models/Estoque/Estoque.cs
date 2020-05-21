using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models.Estoque
{
    public class Estoque
    {
        public int EstoqueId { get; set; }
        [DisplayName("Valor unitário")]
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorIndividual { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [DisplayName("Valor Total")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorTotal { get; set; }
        [Required(ErrorMessage = "Informe o {0} ou crie um novo", AllowEmptyStrings = false)]
        [Display(Name ="Produto")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public List<EntradaSaida> EntradaSaidas { get; set; }
    }
}
