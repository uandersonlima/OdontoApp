using OdontoApp.Models.ClassesRelacionais;
using System.Collections.Generic;


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
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public List<AnamnesesPerguntas> AnamnesesPerguntas { get; set; }
    }
}
