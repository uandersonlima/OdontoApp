using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class TipoPerguntaRepository : ITipoPerguntaRepository
    {
        private readonly OdontoAppContext context;

        public TipoPerguntaRepository(OdontoAppContext context)
        {
            this.context = context;
        }
        public bool HasValue()
        {
            return context.TipoPergunta.Any();
        }
        public async Task<List<TipoPergunta>> GetAllAsync()
        {
            return await context.TipoPergunta.ToListAsync();
        }
        public void Start(List<TipoPergunta> tipoPerguntas)
        {
            context.TipoPergunta.AddRangeAsync(tipoPerguntas);
            context.SaveChanges();
        }
    }
}
