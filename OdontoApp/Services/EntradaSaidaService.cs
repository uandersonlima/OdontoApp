using OdontoApp.Models.Const;
using OdontoApp.Models.Estoque;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using OdontoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class EntradaSaidaService : IEntradaSaidaService
    {
        private readonly IEntradaSaidaRepository entradaSaidaRepos;
        private readonly IEstoqueService estoqueSvc;
        private readonly ILoginService loginSvc;

        public EntradaSaidaService(IEntradaSaidaRepository entradaSaidaRepos, IEstoqueService estoqueSvc, ILoginService loginSvc)
        {
            this.entradaSaidaRepos = entradaSaidaRepos;
            this.estoqueSvc = estoqueSvc;
            this.loginSvc = loginSvc;
        }

        public async Task AddAsync(EntradaSaida entity)
        {
            await entradaSaidaRepos.AddAsync(entity);
        }

        public async Task AddProductAsync(EntradaSaida entity)
        {
            var estoque = await estoqueSvc.GetByIdAsync(entity.EstoqueId);
            if (entity.Quantidade > 0 && entity.Valor > 0)
            {
                estoque.Quantidade += entity.Quantidade;
                estoque.ValorTotal += (entity.Valor * entity.Quantidade);
                entity.DataTransacao = DateTime.Now;
                entity.Estoque = estoque;
                entity.EntradaSaidaId = 0;
                await UpdateAsync(entity);
            }
        }
        public async Task SubstractProductAsync(EntradaSaida entity)
        {
            var estoque = await estoqueSvc.GetByIdAsync(entity.EstoqueId);
            if (entity.Quantidade > 0 && entity.Valor > 0)
            {
                estoque.Quantidade -= entity.Quantidade;
                estoque.ValorTotal -= (entity.Valor * entity.Quantidade);
                entity.DataTransacao = DateTime.Now;
                entity.Estoque = estoque;
                entity.EntradaSaidaId = 0;
                if (estoque.Quantidade >= 0)
                {
                    await UpdateAsync(entity);
                }
            }
        }
        public async Task<bool> CheckEntityAsync(EntradaSaida entity)
        {
            return await entradaSaidaRepos.CheckEntityAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await DeleteAsync(await GetByIdAsync(id));
        }

        public async Task DeleteAsync(EntradaSaida entity)
        {
            await entradaSaidaRepos.DeleteAsync(entity);
        }

        public async Task<List<EntradaSaida>> GetAllAsync()
        {
            return await entradaSaidaRepos.GetAllAsync(new AppView(), loginSvc.GetUser().UsuarioId);
        }

        public async Task<PaginationList<EntradaSaida>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await entradaSaidaRepos.GetAllAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }

        public async Task<PaginationList<EntradaSaida>> GetAllInputsAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await entradaSaidaRepos.GetAllInputsAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }

        public async Task<PaginationList<EntradaSaida>> GetAllOutputsAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await entradaSaidaRepos.GetAllInputsAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }

        public async Task<EntradaSaida> GetByIdAsync(int id)
        {
            return await entradaSaidaRepos.GetByIdAsync(id, loginSvc.GetUser().UsuarioId);
        }
        public async Task UpdateAsync(EntradaSaida entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Entrada ou Saída não existem");
            if (entity.Estoque.Produto.UsuarioId == loginSvc.GetUser().UsuarioId)
                await entradaSaidaRepos.UpdateAsync(entity);
        }
    }


}
