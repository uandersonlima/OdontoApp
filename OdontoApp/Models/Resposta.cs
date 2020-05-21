using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Models
{
    public class Resposta
    {
        public int RespostaId { get; set; }

        [Display(Name = "Resposta")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Informe uma {0}", AllowEmptyStrings = false)]
        public string Descricao1 { get; set; }
        [NotMapped]
        public string Descricao2 { get; set; }
        public List<PerguntaAnamnese> PerguntaAnamneses { get; set; }
    }
}
