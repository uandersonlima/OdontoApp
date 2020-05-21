using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OdontoApp.Models
{
    public class DentesRegiao
    {
        public int DentesRegiaoId { get; set; }
        [Display(Name ="Região Dentária")]
        [Required(ErrorMessage ="Informe o campo {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }
        public List<Tratamento> Tratamentos { get; set; }
        public List<Orcamento> Orcamentos { get; set; }
        public List<Recebimento> Recebimentos { get; set; }
    }
}
