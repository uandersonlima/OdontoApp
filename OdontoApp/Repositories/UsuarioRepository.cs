using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Models.Const;
using OdontoApp.Models.DTO;
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
        private readonly UserManager<ApplicationUser> userManager;

        public UsuarioRepository(OdontoAppContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task AddAsync(ApplicationUser entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(ApplicationUser entity)
        {
            try
            {
                context.Update(entity);
                context.Entry(entity).Property(a => a.Nome).IsModified = false;
                context.Entry(entity).Property(a => a.Email).IsModified = false;
                context.Entry(entity).Property(a => a.AccessType).IsModified = false;
                context.Entry(entity).Property(a => a.Nascimento).IsModified = false;
                context.Entry(entity).Property(a => a.Sexo).IsModified = false;
                context.Entry(entity).Property(a => a.CPF).IsModified = false;
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
        public async Task ChangePasswordAsync(ApplicationUser appUser, string currentPassword, string newPassword)
        {
            await userManager.ChangePasswordAsync(appUser, currentPassword, newPassword);
        }

        public async Task ResetPasswordAsync(ApplicationUser appUser, string token, string newPassword)
        {
            await userManager.ResetPasswordAsync(appUser, token, newPassword);
        }
        public async Task<bool> CheckEntityAsync(ApplicationUser entity)
        {
            return await context.User.AnyAsync(user => user.Id == entity.Id);
        }

        public async Task<List<string>> CreateAsync(ApplicationUser appUser, string password)
        {
            var response = await userManager.CreateAsync(appUser, password);
            if (!response.Succeeded)
            {
                List<string> sb = new List<string>();
                foreach (var erro in response.Errors)
                {
                    sb.Add(erro.Description);
                }
                return sb;
            }
            return null;
        }
        public async Task DeleteAsync(ApplicationUser entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        public async Task<PaginationList<ApplicationUser>> GetAllAsync(AppView appQuery)
        {
            var pagList = new PaginationList<ApplicationUser>();
            var usuarios = context.User.AsNoTracking().AsQueryable();
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

        public async Task<ApplicationUser> FindUserByLoginAsync(SignInUser signInUser)
        {
            var user = await userManager.FindByEmailAsync(signInUser.Username);
            if (user is null)
                return null;
            if (await userManager.CheckPasswordAsync(user, signInUser.Password))
                return user;
            return null;
        }
        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await context.User.AsNoTracking()
                .Include(obj => obj.Endereco)
                .Include(obj => obj.Endereco.Rua)
                .Include(obj => obj.Endereco.Bairro)
                .Include(obj => obj.Endereco.Cidade)
                .Include(obj => obj.Endereco.Estado)
                .Include(obj => obj.Endereco.Cep)
                .Where(user => user.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ValidateEmailAsync(string email)
        {
            return await context.User.AnyAsync(a => a.Email.ToLower() == email.ToLower());
        }

        public async Task<int> NumberOfUserWithADM()
        {
            return await context.User.CountAsync();
        }

        public int NumberOfUserWithoutADM()
        {
            return context.User.Count(user => user.AccessType != AccessType.Administrator);
        }

        public async Task UpdateAsync(ApplicationUser entity)
        {
            try
            {
                context.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task UpdateProfileAsync(ApplicationUser entity)
        {
            try
            {
                context.Update(entity);
                context.Entry(entity).Property(a => a.PaymentStatus).IsModified = false;
                context.Entry(entity).Property(a => a.AccessType).IsModified = false;
                context.Entry(entity).Property(a => a.PlanNumber).IsModified = false;
                context.Entry(entity).Property(a => a.Email).IsModified = false;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser appUser)
        {
            return await userManager.GeneratePasswordResetTokenAsync(appUser);
        }
    }
}
