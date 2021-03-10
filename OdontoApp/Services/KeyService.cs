using OdontoApp.Models;
using OdontoApp.Models.Access;
using OdontoApp.Models.Const;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class KeyService : IKeyService
    {
        private readonly IKeyRepository codigoRepos;
        private readonly IEmailService emailSvc;

        public KeyService(IKeyRepository codigoRepos, IEmailService emailSvc)
        {
            this.codigoRepos = codigoRepos;
            this.emailSvc = emailSvc;
        }

        public async Task AddAsync(AccessKey acessKey)
        {
            var previousCode = await SearchKeyByEmailAndTypeAsync(acessKey);
            if (!(previousCode is null))
            {
                await DeleteAsync(previousCode);
            }
            acessKey.User = null;
            await codigoRepos.AddAsync(acessKey);
        }
        public bool KeyIsValid(string key)
        { 
            Guid x;
            return Guid.TryParse(key, out x);  
        }
        public async Task CreateNewKeyAsync(ApplicationUser entity, string keyType)
        {
            var key = Guid.NewGuid().ToString("N");
            AccessKey newKey = new AccessKey
            {
                Key = key,
                KeyType = keyType,
                DataGerada = DateTime.Now,
                UserId = entity.Id,
                User = entity
            };
            if (keyType == KeyType.Verification)
            {
                emailSvc.SendEmailVerificationAsync(entity, key);
            }
            else if (keyType == KeyType.Recovery)
            {
                emailSvc.SendEmailRecoveryAsync(entity, key);
            }
            await AddAsync(newKey);
        }

        public async Task DeleteAsync(AccessKey acessKey) => await codigoRepos.DeleteAsync(acessKey);

        public async Task<TimeSpan> ElapsedTimeAsync(AccessKey acessKey)
        {
            var previousCodigo = await SearchKeyByEmailAndTypeAsync(acessKey);
            if (!(previousCodigo is null)) 
            {               
                return DateTime.Now.Subtract(previousCodigo.DataGerada);
            }
            return new TimeSpan(0, 15, 0);
        }
        public async Task<AccessKey> SearchKeyByEmailAndTypeAsync(AccessKey acessKey)
        =>
        await codigoRepos.SearchKeyByEmailAndTypeAsync(acessKey);

        public async Task<AccessKey> SearchKeyAsync(string key)
        =>
        await codigoRepos.SearchKeyAsync(key);
        
    }
}
