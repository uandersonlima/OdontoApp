using OdontoApp.Models;
using OdontoApp.Models.Const;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using OdontoApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class PerguntaAnamneseService: IPerguntaAnamneseService
    {
        private readonly IPerguntaAnamneseRepository perguntaAnamneseRepos;
        private readonly ILoginService loginSvc;

        public PerguntaAnamneseService(IPerguntaAnamneseRepository perguntaAnamneseRepos,ILoginService loginSvc)
        {
            this.perguntaAnamneseRepos = perguntaAnamneseRepos;
            this.loginSvc = loginSvc;
        }

        public async Task AddAsync(PerguntaAnamnese entity)
        {
            entity.UsuarioId = loginSvc.GetUser().UsuarioId;
            await perguntaAnamneseRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(PerguntaAnamnese entity) => await perguntaAnamneseRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(PerguntaAnamnese entity)
        {
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await perguntaAnamneseRepos.DeleteAsync(entity);
        }

        public async Task<List<PerguntaAnamnese>> GetAllAsync() => await perguntaAnamneseRepos.GetAllAsync(new AppQuery(), loginSvc.GetUser().UsuarioId);

        public async Task<PerguntaAnamnese> GetByIdAsync(int id) => await perguntaAnamneseRepos.GetByIdAsync(id, loginSvc.GetUser().UsuarioId);

        public async Task<PaginationList<PerguntaAnamnese>> GetAllAsync(AppQuery appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await perguntaAnamneseRepos.GetAllAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }
        public async Task UpdateAsync(PerguntaAnamnese entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Pergunta não existe");
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await perguntaAnamneseRepos.UpdateAsync(entity);
        }
    }
}
