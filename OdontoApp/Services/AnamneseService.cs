using OdontoApp.Models;
using OdontoApp.Models.ClassesRelacionais;
using OdontoApp.Models.Const;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using OdontoApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class AnamneseService : IAnamneseService
    {
        private readonly IAnamneseRepository anamneseRepos;
        private readonly IAuthService authService;

        public AnamneseService(IAnamneseRepository anamneseRepos, IAuthService authService)
        {
            this.anamneseRepos = anamneseRepos;
            this.authService = authService;
        }

        public async Task AddAsync(Anamnese entity, List<int> listPerguntaId)
        {
            entity.AnamnesesPerguntas = new List<AnamnesesPerguntas>();
            listPerguntaId.ForEach(PoAId => entity.AnamnesesPerguntas.Add(new AnamnesesPerguntas { PerguntaAnamneseId = PoAId }));
            await AddAsync(entity);
        }
        public async Task AddAsync(Anamnese entity)
        {
            entity.UsuarioId = authService.GetLoggedUserAsync().Result.Id;
            await anamneseRepos.AddAsync(entity);
        }

        public async Task AddPacienteToAnamneseAsync(Anamnese entity, int pacienteId)
        {
            entity.PacienteId = pacienteId;
            entity.AnamneseId = 0; //cancela ID para adicionar um novo Modelo
            var LoggedUserId = authService.GetLoggedUserAsync().Result.Id;
            entity.AnamnesesPerguntas.ForEach(AoP => AoP.PerguntaAnamnese.UsuarioId = LoggedUserId);
            await AddAsync(entity);
        }

        public async Task<List<int>> CheckBoxChecked(int anamneseId)
        {
            var checkeD = await GetByIdAsync(anamneseId);
            return checkeD.AnamnesesPerguntas.Select(AoP => AoP.PerguntaAnamneseId).ToList();
        }
        public async Task<bool> CheckEntityAsync(Anamnese entity) => await anamneseRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Anamnese entity)
        {
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await anamneseRepos.DeleteAsync(entity);
        }

        public async Task ExcludePacienteAnamnese(int pacienteId, int anamneseId)
        {
            var entity = await GetByIdAsync(anamneseId);
            if (entity.PacienteId == pacienteId)
                await anamneseRepos.DeleteAllAsync(entity);
        }

        public async Task<List<Anamnese>> GetAllAsync() => await anamneseRepos.GetAllAsync(new AppView(), authService.GetLoggedUserAsync().Result.Id);

        public async Task<PaginationList<Anamnese>> GetAllAsync(AppView appQuery)
        {
            return await anamneseRepos.GetAllAsync(appQuery, authService.GetLoggedUserAsync().Result.Id);
        }

        public async Task<Anamnese> GetByIdAsync(int id) => await anamneseRepos.GetByIdAsync(id, authService.GetLoggedUserAsync().Result.Id);

        public async Task UpdateAsync(Anamnese entity)
        {
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await anamneseRepos.UpdateAsync(entity);
        }

        public async Task UpdateAsync(Anamnese entity, List<int> listPerguntaId)
        {
            entity.AnamnesesPerguntas = new List<AnamnesesPerguntas>();
            listPerguntaId.ForEach(PoAId => entity.AnamnesesPerguntas.Add(new AnamnesesPerguntas { PerguntaAnamneseId = PoAId }));
            await UpdateAsync(entity);
        }

        public async Task UpdatePacienteAnamneseAsync(Anamnese entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Anamnese não existe");
            await UpdateAsync(entity);
        }
    }
}