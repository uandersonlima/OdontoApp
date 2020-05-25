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
    public class AgendaService : IAgendaService
    {
        private readonly IAgendaRepository agendaRepos;
        private readonly ILoginService loginSvc;
        public AgendaService(IAgendaRepository agendaRepos, ILoginService loginSvc)
        {
            this.agendaRepos = agendaRepos;
            this.loginSvc = loginSvc;
        }

        public async Task AddAsync(Agenda entity)
        {
            entity.UsuarioId = loginSvc.GetUser().UsuarioId;
            await agendaRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Agenda entity) => await agendaRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Agenda entity)
        {
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await agendaRepos.DeleteAsync(entity);
        }

        public async Task<List<Agenda>> GetAllAsync() => await agendaRepos.GetAllAsync(new AppView(), loginSvc.GetUser().UsuarioId);

        public async Task<Agenda> GetByIdAsync(int id) => await agendaRepos.GetByIdAsync(id, loginSvc.GetUser().UsuarioId);

        public async Task<PaginationList<Agenda>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await agendaRepos.GetAllAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }
        public async Task UpdateAsync(Agenda entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Agendamento não existe");
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await agendaRepos.UpdateAsync(entity);
        }

        public async Task<PaginationList<Agenda>> GetByPatientAsync(AppView appQuery, int pacienteId)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await agendaRepos.GetByPatientAsync(appQuery, pacienteId, loginSvc.GetUser().UsuarioId);
        }
    }
}
