using OdontoApp.Models;
using System.Collections.Generic;

namespace OdontoApp.Repositories.Interfaces
{
    public interface ITipoPerguntaRepository
    {
        bool HasValue();
        void Start(List<TipoPergunta> dentesRegiaos);
    }
}
