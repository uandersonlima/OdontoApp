using OdontoApp.Models;
using OdontoApp.Services.Interfaces;
using System.Collections.Generic;


namespace OdontoApp.Data
{
    public class InicializarDados
    {
        private readonly ITipoPerguntaService tipoPerguntaService;
        private readonly IDentesRegiaoService dentesRegiaoService;
        public InicializarDados(ITipoPerguntaService tipoPerguntaService, IDentesRegiaoService dentesRegiaoService)
        {
            this.tipoPerguntaService = tipoPerguntaService;
            this.dentesRegiaoService = dentesRegiaoService;
        }

        public void Init()
        {
            if (!tipoPerguntaService.HasValue())
            {
                tipoPerguntaService.Start(new List<TipoPergunta>{
                                                new TipoPergunta {DescricaoTipoPergunta = "Sim/Não/Não sei"},
                                                new TipoPergunta {DescricaoTipoPergunta = "Sim/Não/Não sei e Texto"},
                                                new TipoPergunta {DescricaoTipoPergunta = "Somente Texto"},
                                                new TipoPergunta {DescricaoTipoPergunta = "Esquerda/Direita/Não sei"}
                });
            }
            if (!dentesRegiaoService.HasValue())
            {
                dentesRegiaoService.Start(new List<DentesRegiao> {
                                                new DentesRegiao {Descricao = "11"},
                                                new DentesRegiao {Descricao = "12"},
                                                new DentesRegiao {Descricao = "13"},
                                                new DentesRegiao {Descricao = "14"},
                                                new DentesRegiao {Descricao = "15"},
                                                new DentesRegiao {Descricao = "16"},
                                                new DentesRegiao {Descricao = "17"},
                                                new DentesRegiao {Descricao = "18"},
                                                new DentesRegiao {Descricao = "21"},
                                                new DentesRegiao {Descricao = "22"},
                                                new DentesRegiao {Descricao = "23"},
                                                new DentesRegiao {Descricao = "24"},
                                                new DentesRegiao {Descricao = "25"},
                                                new DentesRegiao {Descricao = "26"},
                                                new DentesRegiao {Descricao = "27"},
                                                new DentesRegiao {Descricao = "28"},
                                                new DentesRegiao {Descricao = "31"},
                                                new DentesRegiao {Descricao = "32"},
                                                new DentesRegiao {Descricao = "33"},
                                                new DentesRegiao {Descricao = "34"},
                                                new DentesRegiao {Descricao = "35"},
                                                new DentesRegiao {Descricao = "36"},
                                                new DentesRegiao {Descricao = "37"},
                                                new DentesRegiao {Descricao = "38"},
                                                new DentesRegiao {Descricao = "41"},
                                                new DentesRegiao {Descricao = "42"},
                                                new DentesRegiao {Descricao = "43"},
                                                new DentesRegiao {Descricao = "44"},
                                                new DentesRegiao {Descricao = "45"},
                                                new DentesRegiao {Descricao = "46"},
                                                new DentesRegiao {Descricao = "47"},
                                                new DentesRegiao {Descricao = "48"},
                                                new DentesRegiao {Descricao = "51"},
                                                new DentesRegiao {Descricao = "52"},
                                                new DentesRegiao {Descricao = "53"},
                                                new DentesRegiao {Descricao = "54"},
                                                new DentesRegiao {Descricao = "55"},
                                                new DentesRegiao {Descricao = "61"},
                                                new DentesRegiao {Descricao = "62"},
                                                new DentesRegiao {Descricao = "63"},
                                                new DentesRegiao {Descricao = "64"},
                                                new DentesRegiao {Descricao = "65"},
                                                new DentesRegiao {Descricao = "71"},
                                                new DentesRegiao {Descricao = "72"},
                                                new DentesRegiao {Descricao = "73"},
                                                new DentesRegiao {Descricao = "74"},
                                                new DentesRegiao {Descricao = "75"},
                                                new DentesRegiao {Descricao = "81"},
                                                new DentesRegiao {Descricao = "82"},
                                                new DentesRegiao {Descricao = "83"},
                                                new DentesRegiao {Descricao = "84"},
                                                new DentesRegiao {Descricao = "85"},
                                                new DentesRegiao {Descricao = "Maxila"},
                                                new DentesRegiao {Descricao = "Mandíbula"},
                                                new DentesRegiao {Descricao = "Face"},
                                                new DentesRegiao {Descricao = "Arcada Superior"},
                                                new DentesRegiao {Descricao = "Arcada Inferior"},
                                                new DentesRegiao {Descricao = "Arcadas"}
                });
            }
        }
    }
}

