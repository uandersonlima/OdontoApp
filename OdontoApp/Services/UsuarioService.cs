using Microsoft.Extensions.Options;
using OdontoApp.Models;
using OdontoApp.Models.AccessCode;
using OdontoApp.Models.Const;
using OdontoApp.Models.DTO;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using OdontoApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ICodigoService codigoSvc;
        private readonly AppSettings appsettings;
        private readonly IUsuarioRepository usuarioRepos;

        public UsuarioService(ICodigoService codigoSvc, IOptions<AppSettings> appsettings, IUsuarioRepository usuarioRepos)
        {
            this.codigoSvc = codigoSvc;
            this.appsettings = appsettings.Value;
            this.usuarioRepos = usuarioRepos;
        }

        public async Task<bool> ActiveAccountAsync(ApplicationUser entity, AccessCode accessCode)
        {
            var receivedCode = await codigoSvc.SearchAndValidateCodeAsync(accessCode);
            if (!(receivedCode is null))
            {
                await EnableOrDisableAsync(entity);
                await UpdateAsync(entity);
                await codigoSvc.DeleteAsync(receivedCode);
                return true;
            }
            return false;
        }
        public async Task AddAsync(ApplicationUser entity)
        {
            if (entity.Email.ToLower() == appsettings.SmtpUser.ToLower())
            {
                entity.AccessType = AccessType.Administrator;
            }
            else
            {
                entity.AccessType = AccessType.User;
                entity.EmailConfirmed = true;
            }
            await usuarioRepos.AddAsync(entity);
        }

        public async Task ChangePasswordAsync(ApplicationUser entity)
        {
            await usuarioRepos.ChangePasswordAsync(entity);
        }

        public async Task<bool> ChangePasswordByCodeAsync(ApplicationUser entity, AccessCode accessCode)
        {
            var receivedCode = await codigoSvc.SearchAndValidateCodeAsync(accessCode);
            if (!(receivedCode is null))
            {
                if (!await CheckEntityAsync(entity))
                    throw new NotFoundException("Usuario não existe");
                await ChangePasswordAsync(entity);
                await codigoSvc.DeleteAsync(accessCode);
                return true;
            }
            return false;
        }

        public async Task<bool> CheckEntityAsync(ApplicationUser entity)
        {
            return await usuarioRepos.CheckEntityAsync(entity);
        }

        public async Task<List<string>> CreateAsync(ApplicationUser appUser, string password)
        {
            return await usuarioRepos.CreateAsync(appUser, password);
        }
        public async Task DeleteAsync(string userId)
        {
            await DeleteAsync(await GetByIdAsync(userId));
        }

        public async Task DeleteAsync(ApplicationUser entity)
        {
            await usuarioRepos.DeleteAsync(entity);
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await usuarioRepos.FindByEmailAsync(email);
        }
        public async Task EnableOrDisableAsync(ApplicationUser entity)
        {
            entity.EmailConfirmed = !entity.EmailConfirmed;
            await UpdateAsync(entity);
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await usuarioRepos.GetAllAsync(new AppView());
        }

        public async Task<PaginationList<ApplicationUser>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await usuarioRepos.GetAllAsync(appQuery);
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            return await usuarioRepos.GetByIdAsync(userId);
        }

        public async Task<ApplicationUser> FindUserByLoginAsync(SignInUser signInUser)
        {
            return await usuarioRepos.FindUserByLoginAsync(signInUser);
        }

        //nao implementa async nesse
        public async Task<bool> ValidateEmailAsync(string email)
        {
            return await usuarioRepos.ValidateEmailAsync(email);
        }
        public async Task<int> NumberOfUserWithADM()
        {
            return await usuarioRepos.NumberOfUserWithADM();
        }

        public int NumberOfUserWithoutADM()
        {
            return usuarioRepos.NumberOfUserWithoutADM();
        }

        public async Task UpdateAsync(ApplicationUser entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Usuario não existe");
            await usuarioRepos.UpdateAsync(entity);
        }

        public async Task UpdateProfileAsync(ApplicationUser entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Usuario não existe");
            await usuarioRepos.UpdateProfileAsync(entity);
        }
    }
}
