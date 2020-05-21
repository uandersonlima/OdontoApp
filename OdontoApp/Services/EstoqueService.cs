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
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository estoqueRepos;
        private readonly ILoginService loginSvc;

        public EstoqueService(IEstoqueRepository estoqueRepos, ILoginService loginSvc)
        {
            this.estoqueRepos = estoqueRepos;
            this.loginSvc = loginSvc;
        }

        public async Task AddAsync(Estoque entity)
        {
            var result = entity.ValorIndividual * entity.Quantidade;
            if (entity.ValorTotal == null)
            {
                entity.ValorTotal = result;
            }
            else
            {
                entity.ValorTotal += result;
            }
            await estoqueRepos.AddAsync(entity);
            var lastEstoque = await LastStockAddedAsync();
            lastEstoque.EntradaSaidas.Add(
                new EntradaSaida
                {
                    DataTransacao = DateTime.Now,
                    Quantidade = entity.Quantidade,
                    TransactionType = TransactionType.Input,
                    EstoqueId = lastEstoque.EstoqueId,
                    Valor = entity.ValorIndividual
                }
            );
            await UpdateAsync(lastEstoque);
        }
        public async Task<bool> CheckEntityAsync(Estoque entity)
        {
            return await estoqueRepos.CheckEntityAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await DeleteAsync(await GetByIdAsync(id));
        }

        public async Task DeleteAsync(Estoque entity)
        {
            await estoqueRepos.DeleteAsync(entity);
        }

        public async Task<List<Estoque>> GetAllAsync()
        {
            return await estoqueRepos.GetAllAsync(new AppQuery(), loginSvc.GetUser().UsuarioId);
        }

        public async Task<PaginationList<Estoque>> GetAllAsync(AppQuery appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await estoqueRepos.GetAllAsync(appQuery, loginSvc.GetUser().UsuarioId);
        }

        public async Task<Estoque> GetByIdAsync(int id)
        {
            return await estoqueRepos.GetByIdAsync(id, loginSvc.GetUser().UsuarioId);
        }

        public async Task<Estoque> LastStockAddedAsync()
        {
            return await estoqueRepos.LastStockAddedAsync();
        }

        public async Task UpdateAsync(Estoque entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Estoque não existe");
            if (entity.Produto.UsuarioId == loginSvc.GetUser().UsuarioId)
                await estoqueRepos.UpdateAsync(entity);

        }
    }
}
