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
    public class TratamentoRepository:ITratamentoRepository
    {
        private readonly OdontoAppContext context;

        public TratamentoRepository(OdontoAppContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Tratamento entity)
        {
            await context.Tratamento.AddAsync(entity);
            await context.SaveChangesAsync(); ;
        }
        public async Task<bool> CheckEntityAsync(Tratamento entity)
        {
            return await context.Tratamento.AnyAsync(cnc => cnc.TratamentoId == entity.TratamentoId);
        }
        public async Task DeleteAsync(Tratamento entity)
        {
            context.Tratamento.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<Tratamento> GetByIdAsync(int id, int idUser)
        {
            return await context.Tratamento.Where(cnc => cnc.TratamentoId == id && cnc.UsuarioId == idUser).FirstOrDefaultAsync();
        }
        public async Task<PaginationList<Tratamento>> GetAllAsync(AppQuery appQuery, int idUser)
        {
            var pagList = new PaginationList<Tratamento>();
            var tratamentos = context.Tratamento.Where(cnc => cnc.UsuarioId == idUser).AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                tratamentos = tratamentos.Where(cnc => cnc.NomeTratamento.Contains(appQuery.Search.Trim()));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await tratamentos.CountAsync();
                tratamentos = tratamentos.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await tratamentos.ToListAsync());

            return pagList;
        }
        public async Task UpdateAsync(Tratamento entity)
        {
            try
            {
                context.Tratamento.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
