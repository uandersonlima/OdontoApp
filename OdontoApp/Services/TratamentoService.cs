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
    public class TratamentoService: ITratamentoService
    {
        private readonly ITratamentoRepository tratamentoRepos;
        private readonly IAuthService authService;

        public TratamentoService(ITratamentoRepository tratamentoRepos, IAuthService authService)
        {
            this.tratamentoRepos = tratamentoRepos;
            this.authService= authService;
        }
        public async Task AddAsync(Tratamento entity)
        {
            entity.UsuarioId = authService.GetLoggedUserAsync().Result.Id;
            await tratamentoRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Tratamento entity) => await tratamentoRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Tratamento entity)
        {
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await tratamentoRepos.DeleteAsync(entity);
        }

        public async Task<List<Tratamento>> GetAllAsync() => await tratamentoRepos.GetAllAsync(new AppView(), authService.GetLoggedUserAsync().Result.Id);

        public async Task<Tratamento> GetByIdAsync(int id) => await tratamentoRepos.GetByIdAsync(id, authService.GetLoggedUserAsync().Result.Id);

        public async Task<PaginationList<Tratamento>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await tratamentoRepos.GetAllAsync(appQuery, authService.GetLoggedUserAsync().Result.Id);
        }
        public async Task UpdateAsync(Tratamento entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("tratamento não existe");
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await tratamentoRepos.UpdateAsync(entity);
        }

        public async Task<PaginationList<Tratamento>> GetByPatientAsync(AppView appQuery, int pacienteId)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await tratamentoRepos.GetByPatientAsync(appQuery, pacienteId, authService.GetLoggedUserAsync().Result.Id);
        }
    }
}
