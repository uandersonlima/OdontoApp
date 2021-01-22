using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models.Estoque
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        [DisplayName("Produto")]
        public string Descricao { get; set; }
        public int EstoqueMinimo { get; set; }
        public int EstoqueMaximo { get; set; }
        public string Marca { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        
        public List<Estoque> Estoques { get; set; }
    }
}
