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
        private readonly ILoginService loginSvc;

        public OrcamentoService(IOrcamentoRepository orcamentoRepos, ILoginService loginSvc)
        {
            this.orcamentoRepos = orcamentoRepos;
            this.loginSvc = loginSvc;
        }
        public async Task AddAsync(Orcamento entity, List<int> listTratamentoId)
        {
            listTratamentoId.ForEach(orctratId => entity.OrcamentoTratamentos.Add(new OrcamentoTratamento { TratamentoId = orctratId }));
            await AddAsync(entity);
        }
        public async Task AddAsync(Orcamento entity)
        {
            entity.UsuarioId = loginSvc.GetUser().UsuarioId;
            await orcamentoRepos.AddAsync(entity);
        }
        public async Task<List<int>> CheckBoxChecked(int id)
        {
            var orcamento = await GetByIdAsync(id);
            return (List<int>)orcamento.OrcamentoTratamentos.Select(ot => ot.TratamentoId);
        }
        public async Task<bool> CheckEntityAsync(Orcamento entity) => await orcamentoRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Orcamento entity)
        {
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await orcamentoRepos.DeleteAsync(entity);
        }

        public async Task<List<Orcamento>> GetAllAsync() => await orcamentoRepos.GetAllAsync(new AppView(), loginSvc.GetUser().UsuarioId);
        public async Task<PaginationList<Orcamento>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await orcamentoRepos.GetAllAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }
        public async Task<Orcamento> GetByIdAsync(int id) => await orcamentoRepos.GetByIdAsync(id, loginSvc.GetUser().UsuarioId);

        public async Task<PaginationList<Orcamento>> GetByPatientAsync(AppView appQuery, int pacienteId)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await orcamentoRepos.GetByPatientAsync(appQuery, pacienteId, loginSvc.GetUser().UsuarioId);
        }

        public async Task UpdateAsync(Orcamento entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Orcamento não existe");
            if (entity.UsuarioId == loginSvc.GetUser().UsuarioId)
                await orcamentoRepos.UpdateAsync(entity);
        }

        public async Task UpdateAsync(Orcamento entity, List<int> listTratamentoId)
        {
            listTratamentoId.ForEach(orctratId => entity.OrcamentoTratamentos.Add(new OrcamentoTratamento { TratamentoId = orctratId }));
            await UpdateAsync(entity);
        }
    }
}
