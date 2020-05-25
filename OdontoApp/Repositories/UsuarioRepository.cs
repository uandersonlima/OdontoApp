using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Models.Const;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly OdontoAppContext context;

        public UsuarioRepository(OdontoAppContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Usuario entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(Usuario entity)
        {
            try
            {
                context.Update(entity);
                context.Entry(entity).Property(a => a.Nome).IsModified = false;
                context.Entry(entity).Property(a => a.AccountStatus).IsModified = false;
                context.Entry(entity).Property(a => a.Email).IsModified = false;
                context.Entry(entity).Property(a => a.AccessType).IsModified = false;
                context.Entry(entity).Property(a => a.Nascimento).IsModified = false;
                context.Entry(entity).Property(a => a.Sexo).IsModified = false;
                context.Entry(entity).Property(a => a.CPF).IsModified = false;
                context.Entry(entity).Property(a => a.DDD).IsModified = false;
                context.Entry(entity).Property(a => a.Telefone).IsModified = false;
                context.Entry(entity).Property(a => a.PaymentStatus).IsModified = false;
                context.Entry(entity).Property(a => a.PlanNumber).IsModified = false;
                context.Entry(entity).Property(a => a.EnderecoId).IsModified = false;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task<bool> CheckEntityAsync(Usuario entity)
        {
            return await context.Usuario.AnyAsync(user => user.UsuarioId == entity.UsuarioId);
        }

        public async Task DeleteAsync(Usuario entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<PaginationList<Usuario>> GetAllAsync(AppView appQuery)
        {
            var pagList = new PaginationList<Usuario>();
            var usuarios = context.Usuario.AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                usuarios = usuarios.Where(anm => anm.Nome.Contains(appQuery.Search) || anm.CPF.Contains(appQuery.Search));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await usuarios.CountAsync();
                usuarios = usuarios.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await usuarios.ToListAsync());
            return pagList;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await context.Usuario.AsNoTracking()
                .Include(obj => obj.Endereco)
                .Include(obj => obj.Endereco.Rua)
                .Include(obj => obj.Endereco.Bairro)
                .Include(obj => obj.Endereco.Cidade)
                .Include(obj => obj.Endereco.Estado)
                .Include(obj => obj.Endereco.Cep)
                .Where(user => user.UsuarioId == id)
                .FirstOrDefaultAsync();
        }

        //não pode ser assíncrono por causa do filtro
        public List<Usuario> GetUserByEmail(string email)
        {
            return context.Usuario.AsNoTracking().Where(a => a.Email.ToLower() == email.ToLower()).ToList();
        }
        public async Task<Usuario> GetUserByLogin(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                return null;
            }
            var user = await context.Usuario.Where(m => m.Email.ToLower() == email.ToLower() && m.Senha == senha).FirstOrDefaultAsync();
            return user;
        }

        public async Task<int> NumberOfUserWithADM()
        {
            return await context.Usuario.CountAsync();
        }

        public int NumberOfUserWithoutADM()
        {
            return context.Usuario.Count(user => user.AccessType != AccessType.Administrator);
        }

        public async Task UpdateAsync(Usuario entity)
        {
            try
            {
                context.Update(entity);
                context.Entry(entity).Property(a => a.Senha).IsModified = false;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task UpdateProfileAsync(Usuario entity)
        {
            try
            {
                context.Update(entity);
                context.Entry(entity).Property(a => a.AccountStatus).IsModified = false;
                context.Entry(entity).Property(a => a.PaymentStatus).IsModified = false;
                context.Entry(entity).Property(a => a.AccessType).IsModified = false;
                context.Entry(entity).Property(a => a.PlanNumber).IsModified = false;
                context.Entry(entity).Property(a => a.Senha).IsModified = false;
                context.Entry(entity).Property(a => a.Email).IsModified = false;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
