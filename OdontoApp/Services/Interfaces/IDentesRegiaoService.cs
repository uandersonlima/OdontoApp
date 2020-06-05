using OdontoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IDentesRegiaoService
    {
        bool HasValue();
        Task<List<DentesRegiao>> GetAllAsync();
        void Start(List<DentesRegiao> dentesRegiaos);
    }
}
