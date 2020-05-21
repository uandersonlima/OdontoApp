using OdontoApp.Models;
using System.Collections.Generic;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IDentesRegiaoRepository
    {
        bool HasValue();
        void Start(List<DentesRegiao> dentesRegiaos);
    }
}
