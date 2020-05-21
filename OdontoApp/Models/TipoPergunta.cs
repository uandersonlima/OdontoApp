using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OdontoApp.Models
{
    public class TipoPergunta
    {
        public int TipoPerguntaId { get; set; }

        [Display(Name = "Tipo da Pergunta")]
        [Required(ErrorMessage = "Informe o {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public string DescricaoTipoPergunta { get; set; }
        public List<PerguntaAnamnese> PerguntaAnamneses { get; set; }
    }
}
