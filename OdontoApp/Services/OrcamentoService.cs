using OdontoApp.Models;
using OdontoApp.Models.ClassesRelacionais;
using OdontoApp.Models.Const;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using OdontoApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly IOrcamentoRepository orcamentoRepos;
        private readonly IAuthService authService;

        public OrcamentoService(IOrcamentoRepository orcamentoRepos, IAuthService authService)
        {
            this.orcamentoRepos = orcamentoRepos;
            this.authService = authService;
        }
        public async Task AddAsync(Orcamento entity, List<int> listTratamentoId)
        {
            listTratamentoId.ForEach(orctratId => entity.OrcamentoTratamentos.Add(new OrcamentoTratamento { TratamentoId = orctratId }));
            await AddAsync(entity);
        }
        public async Task AddAsync(Orcamento entity)
        {
            entity.UsuarioId = authService.GetLoggedUserAsync().Result.Id;
            await orcamentoRepos.AddAsync(entity);
        }
        public async Task<List<int>> CheckBoxChecked(int id)
        {
            var orcamento = await GetByIdAsync(id);
            return orcamento.OrcamentoTratamentos.Select(ot => ot.TratamentoId).ToList();
        }
        public async Task<bool> CheckEntityAsync(Orcamento entity) => await orcamentoRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Orcamento entity)
        {
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await orcamentoRepos.DeleteAsync(entity);
        }

        public async Task<List<Orcamento>> GetAllAsync() => await orcamentoRepos.GetAllAsync(new AppView(), authService.GetLoggedUserAsync().Result.Id);
        public async Task<PaginationList<Orcamento>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await orcamentoRepos.GetAllAsync(appQuery, authService.GetLoggedUserAsync().Result.Id);
        }
        public async Task<Orcamento> GetByIdAsync(int id) => await orcamentoRepos.GetByIdAsync(id, authService.GetLoggedUserAsync().Result.Id);

        public async Task<PaginationList<Orcamento>> GetByPatientAsync(AppView appQuery, int pacienteId)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await orcamentoRepos.GetByPatientAsync(appQuery, pacienteId, authService.GetLoggedUserAsync().Result.Id);
        }

        public async Task UpdateAsync(Orcamento entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Orcamento não existe");
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await orcamentoRepos.UpdateAsync(entity);
        }

        public async Task UpdateAsync(Orcamento entity, List<int> listTratamentoId)
        {
            listTratamentoId.ForEach(orctratId => entity.OrcamentoTratamentos.Add(new OrcamentoTratamento { TratamentoId = orctratId }));
            await UpdateAsync(entity);
        }
    }
}
