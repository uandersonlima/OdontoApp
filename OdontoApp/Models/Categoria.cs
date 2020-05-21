using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OdontoApp.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [Display(Name = "Categoria")]
        [DataType(DataType.Text)]
        public string DescricaoCategoria { get; set; }
        public List<Despesa> Despesas { get; set; }
    }
}
