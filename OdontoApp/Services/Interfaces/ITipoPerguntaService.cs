using OdontoApp.Models;
using System.Collections.Generic;

namespace OdontoApp.Services.Interfaces
{
    public interface ITipoPerguntaService
    {
        bool HasValue();
        void Start(List<TipoPergunta> tiposPerguntas);
    }
}
