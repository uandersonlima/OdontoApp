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
        private readonly IAuthService authService;
        public AgendaService(IAgendaRepository agendaRepos, IAuthService authService)
        {
            this.agendaRepos = agendaRepos;
            this.authService = authService;
        }

        public async Task AddAsync(Agenda entity)
        {
            entity.UsuarioId = authService.GetLoggedUserAsync().Result.Id;
            await agendaRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Agenda entity) => await agendaRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Agenda entity)
        {
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await agendaRepos.DeleteAsync(entity);
        }

        public async Task<List<Agenda>> GetAllAsync() => await agendaRepos.GetAllAsync(new AppView(), authService.GetLoggedUserAsync().Result.Id);

        public async Task<Agenda> GetByIdAsync(int id) => await agendaRepos.GetByIdAsync(id, authService.GetLoggedUserAsync().Result.Id);

        public async Task<PaginationList<Agenda>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await agendaRepos.GetAllAsync(appQuery, authService.GetLoggedUserAsync().Result.Id);
        }
        public async Task UpdateAsync(Agenda entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Agendamento não existe");
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await agendaRepos.UpdateAsync(entity);
        }

        public async Task<PaginationList<Agenda>> GetByPatientIdAsync(AppView appQuery, int pacienteId)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await agendaRepos.GetByPatientIdAsync(appQuery, pacienteId, authService.GetLoggedUserAsync().Result.Id);
        }
    }
}
