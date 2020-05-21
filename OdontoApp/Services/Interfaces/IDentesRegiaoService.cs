using OdontoApp.Models;
using System.Collections.Generic;

namespace OdontoApp.Services.Interfaces
{
    public interface IDentesRegiaoService
    {
        bool HasValue();
        void Start(List<DentesRegiao> dentesRegiaos);
    }
}
