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
    public class AgendaRepository : IAgendaRepository
    {
        private readonly OdontoAppContext context;

        public AgendaRepository(OdontoAppContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Agenda entity)
        {
            await context.Agenda.AddAsync(entity);
            await context.SaveChangesAsync(); ;
        }
        public async Task<bool> CheckEntityAsync(Agenda entity)
        {
            return await context.Agenda.AnyAsync(agd => agd.AgendaId == entity.AgendaId);
        }
        public async Task DeleteAsync(Agenda entity)
        {
            context.Agenda.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<Agenda> GetByIdAsync(int id, string idUser)
        {
            return await context.Agenda.Include(obj => obj.Paciente).Where(agd => agd.AgendaId == id && agd.UsuarioId == idUser).FirstOrDefaultAsync();
        }
        public async Task<PaginationList<Agenda>> GetAllAsync(AppView appQuery, string idUser)
        {
            var pagList = new PaginationList<Agenda>();
            var agendas = context.Agenda.Where(agd => agd.UsuarioId == idUser).AsNoTracking().AsQueryable();
            if (appQuery.CheckDate())
            {
                agendas = agendas.Where(agd => agd.Inicio >= appQuery.Start && agd.Inicio <= appQuery.End);
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await agendas.CountAsync();
                agendas = agendas.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await agendas.ToListAsync());

            return pagList;
        }
        public async Task UpdateAsync(Agenda entity)
        {
            try
            {
                context.Agenda.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task<PaginationList<Agenda>> GetByPatientAsync(AppView appQuery,int pacienteId, string userId)
        {
            var pagList = new PaginationList<Agenda>();
            var agendas = context.Agenda.Where(agd => agd.UsuarioId == userId && agd.PacienteId == pacienteId).AsNoTracking().AsQueryable();
            if (appQuery.CheckDate())
            {
                agendas = agendas.Where(agd => agd.Inicio >= appQuery.Start && agd.Inicio <= appQuery.End);
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await agendas.CountAsync();
                agendas = agendas.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await agendas.ToListAsync());

            return pagList;
        }
    }
}
