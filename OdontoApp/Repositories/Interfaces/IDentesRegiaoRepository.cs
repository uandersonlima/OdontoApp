using OdontoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IDentesRegiaoRepository
    {
        bool HasValue();
        Task<List<DentesRegiao>> GetAllAsync();
        void Start(List<DentesRegiao> dentesRegiaos);
    }
}
