using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
        public void Start(List<TipoPergunta> tipoPerguntas)
        {
            context.TipoPergunta.AddRangeAsync(tipoPerguntas);
            context.SaveChanges();
        }
    }
}
