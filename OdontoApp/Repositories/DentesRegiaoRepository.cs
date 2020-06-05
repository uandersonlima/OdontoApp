using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<List<DentesRegiao>> GetAllAsync()
        {
            return await context.DentesRegiao.ToListAsync();
        }
        public void Start(List<DentesRegiao> dentesRegiaos)
        {
            context.DentesRegiao.AddRangeAsync(dentesRegiaos);
            context.SaveChanges();
        }
    }
}
