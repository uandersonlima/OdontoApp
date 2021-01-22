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
        private readonly IAuthService authService;

        public PlanoService(IPlanoRepository planoRepos, IAuthService authService)
        {
            this.planoRepos = planoRepos;
            this.authService = authService;
        }
        public async Task AddAsync(Plano entity)
        {
            entity.UsuarioId = authService.GetLoggedUserAsync().Result.Id;
            await planoRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Plano entity) => await planoRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Plano entity)
        {
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await planoRepos.DeleteAsync(entity);
        }

        public async Task<List<Plano>> GetAllAsync() => await planoRepos.GetAllAsync(new AppView(), authService.GetLoggedUserAsync().Result.Id);

        public async Task<Plano> GetByIdAsync(int id) => await planoRepos.GetByIdAsync(id, authService.GetLoggedUserAsync().Result.Id);

        public async Task<PaginationList<Plano>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await planoRepos.GetAllAsync(appQuery, authService.GetLoggedUserAsync().Result.Id);
        }
        public async Task UpdateAsync(Plano entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Plano não existe");
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await planoRepos.UpdateAsync(entity);
        }
    }
}
