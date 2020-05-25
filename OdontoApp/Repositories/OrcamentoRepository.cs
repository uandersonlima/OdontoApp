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
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly OdontoAppContext context;

        public OrcamentoRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Orcamento entity)
        {
            await context.Orcamento.AddAsync(entity);
            await context.SaveChangesAsync(); ;
        }
        public async Task<bool> CheckEntityAsync(Orcamento entity)
        {
            return await context.Orcamento.AnyAsync(orc => orc.OrcamentoId == entity.OrcamentoId);
        }
        public async Task DeleteAsync(Orcamento entity)
        {
            context.Orcamento.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<PaginationList<Orcamento>> GetAllAsync(AppView appQuery, int idUser)
        {
            var pagList = new PaginationList<Orcamento>();
            var orcamentos = context.Orcamento.Where(orc => orc.UsuarioId == idUser).AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                orcamentos = orcamentos.Where(orc => orc.DescricaoOrcamento.Contains(appQuery.Search.Trim()));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await orcamentos.CountAsync();
                orcamentos = orcamentos.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await orcamentos.ToListAsync());

            return pagList;
        }
        public async Task<Orcamento> GetByIdAsync(int id, int idUser)
        {
            return await context.Orcamento.Include(orc => orc.Plano).Include(plan => plan.OrcamentoTratamentos).ThenInclude(ot => ot.Tratamento).ThenInclude(trat => trat.DentesRegiao)
                    .Include(dr => dr.Medico).Where(p => p.UsuarioId == idUser && p.OrcamentoId == id).FirstOrDefaultAsync();
        }
        public async Task<PaginationList<Orcamento>> GetByPatientAsync(AppView appQuery, int pacienteId, int userId)
        {
            var pagList = new PaginationList<Orcamento>();
            var orcamentos = context.Orcamento.Where(p => p.UsuarioId == userId && p.PacienteId == pacienteId).AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                orcamentos = orcamentos.Where(orc => orc.DescricaoOrcamento.Contains(appQuery.Search.Trim()));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await orcamentos.CountAsync();
                orcamentos = orcamentos.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await orcamentos.ToListAsync());
            return pagList;
        }
        public async Task UpdateAsync(Orcamento entity)
        {
            try
            {
                var orcTrat = await context.OrcamentosTratamentos.Where(p => p.OrcamentoId == entity.OrcamentoId).ToListAsync();
                context.OrcamentosTratamentos.RemoveRange(orcTrat);
                context.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
