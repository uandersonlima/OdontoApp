using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OdontoApp.Models.Estoque;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Models.ViewModel
{   
    [NotMapped]
    public class EstoqueViewModel
    {
        public List<Estoque.Estoque> Estoques { get; set; }
        public List<Produto> Produtos { get; set; }
        public List<EntradaSaida> EntradaSaidas { get; set; }
    }
}
