using System.Collections.Generic;
using System.ComponentModel;


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
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Estoque> Estoques { get; set; }
    }
}
