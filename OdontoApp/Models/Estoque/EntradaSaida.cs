using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace OdontoApp.Models.Estoque
{
    public class EntradaSaida
    {
        public int EntradaSaidaId { get; set; }
        public string TransactionType { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataTransacao { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Valor { get; set; }
        public int EstoqueId { get; set; }
        public Estoque Estoque { get; set; }
    }
}
