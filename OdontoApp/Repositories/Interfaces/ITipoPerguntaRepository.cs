using OdontoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface ITipoPerguntaRepository
    {
        bool HasValue();
        Task<List<TipoPergunta>> GetAllAsync();
        void Start(List<TipoPergunta> dentesRegiaos);
    }
}
