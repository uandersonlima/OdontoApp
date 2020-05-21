using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace OdontoApp.Models
{
    public class Rua
    {
        public int RuaId { get; set; }

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "Informe o {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public string Descricao { get; set; }
        [JsonIgnore]
        public List<Endereco> Enderecos { get; set; }
    }
}

