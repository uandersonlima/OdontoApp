using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models.Estoque;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly OdontoAppContext context;

        public EstoqueRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Estoque entity)
        {
            await context.Estoque.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> CheckEntityAsync(Estoque entity)
        {
            return await context.Estoque.AnyAsync(est => est.EstoqueId == entity.EstoqueId);
        }

        public async Task DeleteAsync(Estoque entity)
        {
            context.Estoque.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<PaginationList<Estoque>> GetAllAsync(AppView appQuery, string idUser)
        {
            var pagList = new PaginationList<Estoque>();
            var estoques = context.Estoque.
                                Include(est => est.Produto).
                                Where(est => est.Produto.UsuarioId == idUser).
                                AsNoTracking().
                                AsQueryable();

            if (appQuery.CheckSearch())
            {
                estoques = estoques.Where(est => est.Produto.Descricao.Contains(appQuery.Search));
            }

            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await estoques.CountAsync();
                estoques = estoques.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }

            pagList.AddRange(await estoques.ToListAsync());
            return pagList;
        }

        public async Task<Estoque> GetByIdAsync(int id, string idUser)
        {
            return await context.Estoque.Include(est => est.Produto).Where(est => est.Produto.UsuarioId == idUser && est.EstoqueId == id).FirstOrDefaultAsync();
        }

        public async Task<Estoque> LastStockAddedAsync()
        {
            return await context.Estoque.LastOrDefaultAsync();
        }

        public async Task UpdateAsync(Estoque entity)
        {
            try
            {
                context.Estoque.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
