using Microsoft.Extensions.Configuration;
using OdontoApp.Models;
using OdontoApp.Models.AccessCode;
using OdontoApp.Models.Const;
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
        private readonly IConfiguration conf;
        private readonly IUsuarioRepository usuarioRepos;
        public UsuarioService(IUsuarioRepository usuarioRepos, ICodigoService codigoSvc, IConfiguration conf)
        {
            this.conf = conf;
            this.codigoSvc = codigoSvc; 
            this.usuarioRepos = usuarioRepos;
        }

        public async Task<bool> ActiveAccountAsync(Usuario entity, AccessCode accessCode)
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
        public async Task AddAsync(Usuario entity)
        {
            if (entity.Email.ToLower() == conf.GetValue<string>("Email:Username").ToLower())
            {
                entity.AccessType = AccessType.Administrator;
                entity.AccountStatus = Status.Enabled;
            }
            else
            {
                entity.AccessType = AccessType.User;
                entity.AccountStatus = Status.Disabled;
            }
            await usuarioRepos.AddAsync(entity);
        }

        public async Task ChangePasswordAsync(Usuario entity)
        {
            await usuarioRepos.ChangePasswordAsync(entity);
        }

        public async Task<bool> ChangePasswordByCodeAsync(Usuario entity, AccessCode accessCode)
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

        public async Task<bool> CheckEntityAsync(Usuario entity)
        {
            return await usuarioRepos.CheckEntityAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await DeleteAsync(await GetByIdAsync(id));
        }

        public async Task DeleteAsync(Usuario entity)
        {
            await usuarioRepos.DeleteAsync(entity);
        }

        public async Task EnableOrDisableAsync(Usuario entity)
        {
            entity.AccountStatus = entity.AccountStatus is Status.Disabled ? Status.Enabled : Status.Disabled;
            await UpdateAsync(entity);
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
           return await usuarioRepos.GetAllAsync(new AppView());
        }

        public async Task<PaginationList<Usuario>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await usuarioRepos.GetAllAsync(appQuery);
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await usuarioRepos.GetByIdAsync(id);
        }
        //nao implementa async nesse
        public List<Usuario> GetUserByEmail(string email)
        {
            return usuarioRepos.GetUserByEmail(email);
        }

        public async Task<Usuario> GetUserByLogin(string email, string senha)
        {
            return await usuarioRepos.GetUserByLogin(email, senha);
        }

        public async Task<int> NumberOfUserWithADM()
        {
            return await usuarioRepos.NumberOfUserWithADM();
        }

        public int NumberOfUserWithoutADM()
        {
            return usuarioRepos.NumberOfUserWithoutADM();
        }

        public async Task UpdateAsync(Usuario entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Usuario não existe");
            await usuarioRepos.UpdateAsync(entity);
        }

        public async Task UpdateProfileAsync(Usuario entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Usuario não existe");
            await usuarioRepos.UpdateProfileAsync(entity);
        }
    }
}
