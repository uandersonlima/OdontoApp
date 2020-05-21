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
        private readonly ILoginService loginSvc;

        public TratamentoService(ITratamentoRepository tratamentoRepos, ILoginService loginSvc)
        {
            this.tratamentoRepos = tratamentoRepos;
            this.loginSvc = loginSvc;
        }
        public async Task AddAsync(Tratamento entity)
        {
            entity.UsuarioId = loginSvc.GetUser().UsuarioId;
            await tratamentoRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Tratamento entity) => await tratamentoRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Tratamento entity)
        {
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await tratamentoRepos.DeleteAsync(entity);
        }

        public async Task<List<Tratamento>> GetAllAsync() => await tratamentoRepos.GetAllAsync(new AppQuery(), loginSvc.GetUser().UsuarioId);

        public async Task<Tratamento> GetByIdAsync(int id) => await tratamentoRepos.GetByIdAsync(id, loginSvc.GetUser().UsuarioId);

        public async Task<PaginationList<Tratamento>> GetAllAsync(AppQuery appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await tratamentoRepos.GetAllAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }
        public async Task UpdateAsync(Tratamento entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("tratamento não existe");
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await tratamentoRepos.UpdateAsync(entity);
        }
    }
}
