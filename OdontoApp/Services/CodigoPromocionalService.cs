using OdontoApp.Models.Const;
using OdontoApp.Models.Helpers;
using OdontoApp.Models.Promocoes;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using OdontoApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class CodigoPromocionalService : ICodigoPromocionalService
    {
        private readonly ICodigoPromocionalRepository codigoPromocionalRepos;

        public CodigoPromocionalService(ICodigoPromocionalRepository codigoPromocionalRepos)
        {
            this.codigoPromocionalRepos = codigoPromocionalRepos;
        }

        public async Task AddAsync(CodigoPromocional entity)
        {
            await codigoPromocionalRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(CodigoPromocional entity)
        {
            return await codigoPromocionalRepos.CheckEntityAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await DeleteAsync(await GetByIdAsync(id));
        }

        public async Task DeleteAsync(CodigoPromocional entity)
        {
            await codigoPromocionalRepos.DeleteAsync(entity);
        }

        public async Task<List<CodigoPromocional>> GetAllAsync()
        {
            return await codigoPromocionalRepos.GetAllAsync(new AppQuery());
        }

        public async Task<PaginationList<CodigoPromocional>> GetAllAsync(AppQuery appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await codigoPromocionalRepos.GetAllAsync(appQuery);
        }

        public async Task<CodigoPromocional> GetByIdAsync(int id)
        {
            return await codigoPromocionalRepos.GetByIdAsync(id);
        }

        public async Task<CodigoPromocional> GetByPlanAsync(string planCode)
        {
            return await codigoPromocionalRepos.GetByPlanAsync(planCode);
        }

        public async Task UpdateAsync(CodigoPromocional entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Anamnese não existe");
            await codigoPromocionalRepos.UpdateAsync(entity);
        }

        public async Task<string> VerificarCodigoPromocional(string planCode)
        {
            var promocao = await GetByPlanAsync(planCode);
            if (promocao is null || promocao.Quantidade == 0)
            {
                return StandardPlan.Plan;
            }
            else if (promocao.Quantidade > 0)
            {
                promocao.Quantidade--;
                await UpdateAsync(promocao);
                return promocao.IndentificadorPlano;
            }
            return StandardPlan.Plan;
        }
    }
}
