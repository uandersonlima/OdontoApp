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
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository medicoRepos;
        private readonly ILoginService loginSvc;

        public MedicoService(IMedicoRepository medicoRepos, ILoginService loginSvc)
        {
            this.medicoRepos = medicoRepos;
            this.loginSvc = loginSvc;
        }
        public async Task AddAsync(Medico entity)
        {
            entity.UsuarioId = loginSvc.GetUser().UsuarioId;
            await medicoRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Medico entity) => await medicoRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Medico entity)
        {
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await medicoRepos.DeleteAsync(entity);
        }

        public async Task<List<Medico>> GetAllAsync() => await medicoRepos.GetAllAsync(new AppView(), loginSvc.GetUser().UsuarioId);

        public async Task<Medico> GetByIdAsync(int id) => await medicoRepos.GetByIdAsync(id, loginSvc.GetUser().UsuarioId);

        public async Task<PaginationList<Medico>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await medicoRepos.GetAllAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }
        public async Task UpdateAsync(Medico entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Médico não existe");
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await medicoRepos.UpdateAsync(entity);
        }
    }
}
