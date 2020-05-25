using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class PlanoRepository: IPlanoRepository
    {
        private readonly OdontoAppContext context;

        public PlanoRepository(OdontoAppContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Plano entity)
        {
            await context.Plano.AddAsync(entity);
            await context.SaveChangesAsync(); ;
        }
        public async Task<bool> CheckEntityAsync(Plano entity)
        {
            return await context.Plano.AnyAsync(cnc => cnc.PlanoId == entity.PlanoId);
        }
        public async Task DeleteAsync(Plano entity)
        {
            context.Plano.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<Plano> GetByIdAsync(int id, int idUser)
        {
            return await context.Plano.Where(cnc => cnc.PlanoId == id && cnc.UsuarioId == idUser).FirstOrDefaultAsync();
        }
        public async Task<PaginationList<Plano>> GetAllAsync(AppView appQuery, int idUser)
        {
            var pagList = new PaginationList<Plano>();
            var planos = context.Plano.Where(cnc => cnc.UsuarioId == idUser).AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                planos = planos.Where(cnc => cnc.NomePlano.Contains(appQuery.Search.Trim()) || cnc.NumeroPlano.Contains(appQuery.Search.Trim()));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await planos.CountAsync();
                planos = planos.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await planos.ToListAsync());

            return pagList;
        }
        public async Task UpdateAsync(Plano entity)
        {
            try
            {
                context.Plano.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
