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
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository pacienteRepos;
        private readonly ILoginService loginSvc;
        public PacienteService(IPacienteRepository pacienteRepos, ILoginService loginSvc)
        {
            this.pacienteRepos = pacienteRepos;
            this.loginSvc = loginSvc;
        }

        public async Task AddAsync(Paciente entity)
        {
            entity.UsuarioId = loginSvc.GetUser().UsuarioId;
            await pacienteRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Paciente entity) => await pacienteRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Paciente entity)
        {
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await pacienteRepos.DeleteAsync(entity);
        }

        public async Task<List<Paciente>> GetAllAsync() => await pacienteRepos.GetAllAsync(new AppView(), loginSvc.GetUser().UsuarioId);

        public async Task<Paciente> GetByIdAsync(int id) => await pacienteRepos.GetByIdAsync(id, loginSvc.GetUser().UsuarioId);

        public async Task<PaginationList<Paciente>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await pacienteRepos.GetAllAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }
        public async Task UpdateAsync(Paciente entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Paciente não existe");
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await pacienteRepos.UpdateAsync(entity);
        }
    }
}
