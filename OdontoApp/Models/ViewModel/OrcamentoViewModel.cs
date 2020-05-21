using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models.ViewModel
{
    [NotMapped]
    public class OrcamentoViewModel
    {
        public Orcamento Orcamento { get; set; }
        public List<Tratamento> Tratamentos { get; set; }
    }
}
