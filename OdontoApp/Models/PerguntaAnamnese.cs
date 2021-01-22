using OdontoApp.Models.ClassesRelacionais;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class PerguntaAnamnese
    {
        public int PerguntaAnamneseId { get; set; }
        public int TipoPerguntaId { get; set; }
        public TipoPergunta TipoPergunta { get; set; }
        public int PerguntaId { get; set; }
        public Pergunta Pergunta { get; set; }
        public int? RespostaId { get; set; }
        public Resposta Resposta { get; set; }
        
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

        public List<AnamnesesPerguntas> AnamnesesPerguntas { get; set; }
    }
}
