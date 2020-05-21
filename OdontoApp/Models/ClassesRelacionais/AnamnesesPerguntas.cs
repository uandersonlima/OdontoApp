namespace OdontoApp.Models.ClassesRelacionais
{
    public class AnamnesesPerguntas
    {
        public int AnamneseId { get; set; }
        public Anamnese Anamnese { get; set; }
        public int PerguntaAnamneseId { get; set; }
        public PerguntaAnamnese PerguntaAnamnese { get; set; }
    }
}
