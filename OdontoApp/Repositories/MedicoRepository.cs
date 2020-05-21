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
    public class MedicoRepository: IMedicoRepository
    {
        private readonly OdontoAppContext context;

        public MedicoRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Medico entity)
        {
            await context.Medico.AddAsync(entity);
            await context.SaveChangesAsync(); ;
        }
        public async Task<bool> CheckEntityAsync(Medico entity)
        {
            return await context.Medico.AnyAsync(md => md.MedicoId == entity.MedicoId);
        }
        public async Task DeleteAsync(Medico entity)
        {
            context.Medico.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<Medico> GetByIdAsync(int id, int idUser)
        {
            return await context.Medico.Where(md => md.MedicoId == id && md.UsuarioId == idUser).FirstOrDefaultAsync();
        }
        public async Task<PaginationList<Medico>> GetAllAsync(AppQuery appQuery, int idUser)
        {
            var pagList = new PaginationList<Medico>();
            var medicos = context.Medico.Where(cnc => cnc.UsuarioId == idUser).AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                medicos = medicos.Where(md => md.NomeMedico.Contains(appQuery.Search.Trim()) || md.NumeroCroMedico.Contains(appQuery.Search.Trim()));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await medicos.CountAsync();
                medicos = medicos.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await medicos.ToListAsync());

            return pagList;
        }
        public async Task UpdateAsync(Medico entity)
        {
            try
            {
                context.Medico.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
