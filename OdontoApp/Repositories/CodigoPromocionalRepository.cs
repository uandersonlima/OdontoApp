using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models.Helpers;
using OdontoApp.Models.Promocoes;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class CodigoPromocionalRepository : ICodigoPromocionalRepository
    {
        private readonly OdontoAppContext context;

        public CodigoPromocionalRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(CodigoPromocional entity)
        {
            await context.CodigosPromocionais.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> CheckEntityAsync(CodigoPromocional entity)
        {
            return await context.CodigosPromocionais.AnyAsync(CoP => CoP.CodigoPromocionalId == entity.CodigoPromocionalId);
        }

        public async Task DeleteAsync(CodigoPromocional entity)
        {
            context.CodigosPromocionais.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<PaginationList<CodigoPromocional>> GetAllAsync(AppView appQuery)
        {
            var pagList = new PaginationList<CodigoPromocional>();
            var codigosPromocionais = context.CodigosPromocionais.AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                codigosPromocionais = codigosPromocionais.Where(CoP => CoP.Codigo.Contains(appQuery.Search) || CoP.IndentificadorPlano.Contains(appQuery.Search));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await codigosPromocionais.CountAsync();
                codigosPromocionais = codigosPromocionais.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await codigosPromocionais.ToListAsync());
            return pagList;
        }

        public async Task<CodigoPromocional> GetByIdAsync(int id)
        {
            return await context.CodigosPromocionais.Where(obj => obj.CodigoPromocionalId == id).FirstOrDefaultAsync();          
        }

        public async Task<CodigoPromocional> GetByPlanAsync(string planCode)
        {
            return await context.CodigosPromocionais.Where(x => x.Codigo.CompareTo(planCode) == 0).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(CodigoPromocional entity)
        {
            try
            {
                context.CodigosPromocionais.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
