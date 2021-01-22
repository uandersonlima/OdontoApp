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
    public class ClinicaService : IClinicaService
    {
        private readonly IClinicaRepository clinicaRepos;
        private readonly IAuthService authService;

        public ClinicaService(IClinicaRepository clinicaRepos, IAuthService authService)
        {
            this.clinicaRepos = clinicaRepos;
            this.authService = authService;
        }

        public async Task AddAsync(Clinica entity)
        {
            entity.UsuarioId = authService.GetLoggedUserAsync().Result.Id;
            await clinicaRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Clinica entity) => await clinicaRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Clinica entity)
        {
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await clinicaRepos.DeleteAsync(entity);
        }

        public async Task<List<Clinica>> GetAllAsync() => await clinicaRepos.GetAllAsync(new AppView(), authService.GetLoggedUserAsync().Result.Id);

        public async Task<Clinica> GetByIdAsync(int id) => await clinicaRepos.GetByIdAsync(id, authService.GetLoggedUserAsync().Result.Id);

        public async Task<PaginationList<Clinica>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await clinicaRepos.GetAllAsync(appQuery, authService.GetLoggedUserAsync().Result.Id);
        }
        public async Task UpdateAsync(Clinica entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Clinica não existe");
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await clinicaRepos.UpdateAsync(entity);
        }
    }
}
