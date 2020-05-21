using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdontoApp.Models
{
    public class Pergunta
    {
        public int PerguntaId { get; set; }

        [Display(Name = "Pergunta")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Informe o Campo {0}", AllowEmptyStrings = false)]
        public string DescricaoPergunta { get; set; }
        public List<PerguntaAnamnese> PerguntaAnamneses { get; set; }
    }
}
