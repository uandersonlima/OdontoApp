using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OdontoApp.Models
{
    public class Cidade
    {
        public int CidadeId { get; set; }
        [Display(Name = "Cidade")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="informe o Campo {0}", AllowEmptyStrings = false)]
        public string Descricao { get; set; }
        [JsonIgnore]
        public List<Endereco> Enderecos { get; set; }
    }
}
