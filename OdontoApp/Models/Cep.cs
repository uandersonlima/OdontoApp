using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OdontoApp.Models
{
    public class Cep
    {
        public int CepId { get; set; }
        [Display(Name = "CEP")]
        [Required(ErrorMessage = "Preencha o campo", AllowEmptyStrings = false)]
        [MinLength(10, ErrorMessage = "Campo deve conter 8 dígitos")]
        [MaxLength(10, ErrorMessage = "Campo deve conter 8 dígitos")]
        public string Descricao { get; set; }
        [JsonIgnore]
        public List<Endereco> Enderecos { get; set; }
    }
}
