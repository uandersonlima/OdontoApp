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
    public class ProdutoRepository: IProdutoRepository
    {
        private readonly OdontoAppContext context;

        public ProdutoRepository(OdontoAppContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Produto entity)
        {
            await context.Produto.AddAsync(entity);
            await context.SaveChangesAsync(); ;
        }
        public async Task<bool> CheckEntityAsync(Produto entity)
        {
            return await context.Produto.AnyAsync(cnc => cnc.ProdutoId == entity.ProdutoId);
        }
        public async Task DeleteAsync(Produto entity)
        {
            context.Produto.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<Produto> GetByIdAsync(int id, string idUser)
        {
            return await context.Produto.Where(cnc => cnc.ProdutoId == id && cnc.UsuarioId == idUser).FirstOrDefaultAsync();
        }
        public async Task<PaginationList<Produto>> GetAllAsync(AppView appQuery, string idUser)
        {
            var pagList = new PaginationList<Produto>();
            var produtos = context.Produto.Where(cnc => cnc.UsuarioId == idUser).AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                produtos = produtos.Where(cnc => cnc.Descricao.Contains(appQuery.Search.Trim()));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await produtos.CountAsync();
                produtos = produtos.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await produtos.ToListAsync());

            return pagList;
        }
        public async Task UpdateAsync(Produto entity)
        {
            try
            {
                context.Produto.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
