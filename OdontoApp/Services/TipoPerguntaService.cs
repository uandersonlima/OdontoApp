using OdontoApp.Models;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Interfaces;
using System.Collections.Generic;

namespace OdontoApp.Services
{
    public class TipoPerguntaService : ITipoPerguntaService
    {
        private readonly ITipoPerguntaRepository tipoPerguntaRepos;

        public TipoPerguntaService(ITipoPerguntaRepository tipoPerguntaRepos)
        {
            this.tipoPerguntaRepos = tipoPerguntaRepos;
        }

        public bool HasValue()
        {
            return tipoPerguntaRepos.HasValue();
        }

        public void Start(List<TipoPergunta> tiposPerguntas)
        {
            tipoPerguntaRepos.Start(tiposPerguntas);
        }
    }
}
