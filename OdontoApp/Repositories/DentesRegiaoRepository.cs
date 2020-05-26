using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OdontoApp.Repositories
{
    public class DentesRegiaoRepository : IDentesRegiaoRepository
    {
        private readonly OdontoAppContext context;

        public DentesRegiaoRepository(OdontoAppContext context)
        {
            this.context = context;
        }
        public bool HasValue()
        {
            return context.DentesRegiao.Any();
        }
        public void Start(List<DentesRegiao> dentesRegiaos)
        {
            context.DentesRegiao.AddRangeAsync(dentesRegiaos);
            context.SaveChanges();
        }
    }
}
