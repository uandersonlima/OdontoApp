using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace OdontoApp.Models.ViewModel
{
    [NotMapped]
    public class AnamneseFormViewModel
    {
        public Anamnese Anamnese{get; set;}
        public List<PerguntaAnamnese> Perguntas { get; set; }
    }
}
