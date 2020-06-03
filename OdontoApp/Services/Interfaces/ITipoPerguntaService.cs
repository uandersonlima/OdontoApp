using OdontoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface ITipoPerguntaService
    {
        bool HasValue();
        Task<List<TipoPergunta>> GetAllAsync();
        void Start(List<TipoPergunta> tiposPerguntas);
    }
}
