using OdontoApp.Models;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class DentesRegiaoService : IDentesRegiaoService
    {
        private readonly IDentesRegiaoRepository dentesRegiaoRepos;

        public DentesRegiaoService(IDentesRegiaoRepository dentesRegiaoRepos)
        {
            this.dentesRegiaoRepos = dentesRegiaoRepos;
        }

        public bool HasValue()
        {
            return dentesRegiaoRepos.HasValue();
        }

        public async Task<List<DentesRegiao>> GetAllAsync()
        {
            return await dentesRegiaoRepos.GetAllAsync();
        }

        public void Start(List<DentesRegiao> dentesRegiaos)
        {
            dentesRegiaoRepos.Start(dentesRegiaos);
        }
    }
}
