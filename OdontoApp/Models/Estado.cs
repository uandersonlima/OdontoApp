using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OdontoApp.Models
{
    public class Estado
    {
        public int EstadoId { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Informe o {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }
        [JsonIgnore]
        public List<Endereco> Enderecos { get; set; }
    }
}
