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
    public class PlanoService : IPlanoService
    {
        private readonly IPlanoRepository planoRepos;
        private readonly ILoginService loginSvc;

        public PlanoService(IPlanoRepository planoRepos, ILoginService loginSvc)
        {
            this.planoRepos = planoRepos;
            this.loginSvc = loginSvc;
        }
        public async Task AddAsync(Plano entity)
        {
            entity.UsuarioId = loginSvc.GetUser().UsuarioId;
            await planoRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Plano entity) => await planoRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Plano entity)
        {
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await planoRepos.DeleteAsync(entity);
        }

        public async Task<List<Plano>> GetAllAsync() => await planoRepos.GetAllAsync(new AppQuery(), loginSvc.GetUser().UsuarioId);

        public async Task<Plano> GetByIdAsync(int id) => await planoRepos.GetByIdAsync(id, loginSvc.GetUser().UsuarioId);

        public async Task<PaginationList<Plano>> GetAllAsync(AppQuery appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await planoRepos.GetAllAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }
        public async Task UpdateAsync(Plano entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Plano não existe");
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await planoRepos.UpdateAsync(entity);
        }
    }
}
