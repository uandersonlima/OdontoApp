using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OdontoApp.Models
{
    public class Bairro
    {
        public int BairroId { get; set; }
        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Infome o campo {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }
        [JsonIgnore]
        public List<Endereco> Enderecos { get; set; }
    }
}
