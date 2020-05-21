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
        private readonly ILoginService loginSvc;

        public ClinicaService(IClinicaRepository clinicaRepos, ILoginService loginSvc)
        {
            this.clinicaRepos = clinicaRepos;
            this.loginSvc = loginSvc;
        }

        public async Task AddAsync(Clinica entity)
        {
            entity.UsuarioId = loginSvc.GetUser().UsuarioId;
            await clinicaRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Clinica entity) => await clinicaRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Clinica entity)
        {
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await clinicaRepos.DeleteAsync(entity);
        }

        public async Task<List<Clinica>> GetAllAsync() => await clinicaRepos.GetAllAsync(new AppQuery(), loginSvc.GetUser().UsuarioId);

        public async Task<Clinica> GetByIdAsync(int id) => await clinicaRepos.GetByIdAsync(id, loginSvc.GetUser().UsuarioId);

        public async Task<PaginationList<Clinica>> GetAllAsync(AppQuery appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await clinicaRepos.GetAllAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }
        public async Task UpdateAsync(Clinica entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Clinica não existe");
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await clinicaRepos.UpdateAsync(entity);
        }
    }
}
