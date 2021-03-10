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
    public class PacienteRepository : IPacienteRepository
    {
        private readonly OdontoAppContext context;

        public PacienteRepository(OdontoAppContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Paciente entity)
        {
            await context.Paciente.AddAsync(entity);
            await context.SaveChangesAsync(); ;
        }
        public async Task<bool> CheckEntityAsync(Paciente entity)
        {
            return await context.Paciente.AnyAsync(cnc => cnc.PacienteId == entity.PacienteId);
        }
        public async Task DeleteAsync(Paciente entity)
        {
            context.Paciente.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<Paciente> GetByIdAsync(int id, string userId)
        {
            return await context.Paciente.Where(cnc => cnc.PacienteId == id && cnc.UsuarioId == userId)
                                         .Include(paciente => paciente.Plano)
                                         .Include(paciente => paciente.Endereco)
                                         .Include(paciente => paciente.Endereco.Rua)
                                         .Include(paciente => paciente.Endereco.Cep)
                                         .Include(paciente => paciente.Endereco.Estado)
                                         .Include(paciente => paciente.Endereco.Bairro)
                                         .Include(paciente => paciente.Endereco.Cidade)
                                         .FirstOrDefaultAsync();
        }
        public async Task<PaginationList<Paciente>> GetAllAsync(AppView appQuery, string userId)
        {
            var pagList = new PaginationList<Paciente>();
            var pacientes = context.Paciente.Where(ent => ent.UsuarioId == userId).OrderBy(pac => pac.NomePaciente).ThenBy(pac => pac.NomePaciente).AsNoTracking().AsQueryable();

    
            if (appQuery.CheckSearch())
            {
                pacientes = pacientes.Where(paciente => paciente.NomePaciente.Contains(appQuery.Search.Trim()) || paciente.EmailPaciente.Contains(appQuery.Search.Trim()) || paciente.CPF.Contains(appQuery.Search.Trim()));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await pacientes.CountAsync();
                pacientes = pacientes.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await pacientes.ToListAsync());
            return pagList;
        }
        public async Task UpdateAsync(Paciente entity)
        {
            try
            {
                context.Paciente.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
