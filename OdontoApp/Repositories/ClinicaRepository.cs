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
    public class ClinicaRepository : IClinicaRepository
    {
        private readonly OdontoAppContext context;

        public ClinicaRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Clinica entity)
        {
            await context.Clinica.AddAsync(entity);
            await context.SaveChangesAsync(); ;
        }
        public async Task<bool> CheckEntityAsync(Clinica entity)
        {
            return await context.Clinica.AnyAsync(cnc => cnc.ClinicaId == entity.ClinicaId);
        }
        public async Task DeleteAsync(Clinica entity)
        {
            context.Clinica.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<Clinica> GetByIdAsync(int id, int idUser)
        {
            return await context.Clinica.Where(cnc => cnc.ClinicaId == id && cnc.UsuarioId == idUser).FirstOrDefaultAsync();
        }
        public async Task<PaginationList<Clinica>> GetAllAsync(AppQuery appQuery, int idUser)
        {
            var pagList = new PaginationList<Clinica>();
            var clinicas = context.Clinica.Where(cnc => cnc.UsuarioId == idUser).AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                clinicas = clinicas.Where(cnc => cnc.NomeClinica.Contains(appQuery.Search.Trim()));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await clinicas.CountAsync();
                clinicas = clinicas.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await clinicas.ToListAsync());

            return pagList;
        }
        public async Task UpdateAsync(Clinica entity)
        {
            try
            {
                context.Clinica.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
