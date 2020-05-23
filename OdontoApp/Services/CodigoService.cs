using OdontoApp.Libraries.Seguranca;
using OdontoApp.Libraries.Texto;
using OdontoApp.Models;
using OdontoApp.Models.AccessCode;
using OdontoApp.Models.Const;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class CodigoService : ICodigoService
    {
        private readonly ICodigoRepository codigoRepos;
        private readonly IEmailService emailSvc;

        public CodigoService(ICodigoRepository codigoRepos, IEmailService emailSvc)
        {
            this.codigoRepos = codigoRepos;
            this.emailSvc = emailSvc;
        }

        public async Task AddAsync(AccessCode acessCode)
        {
            var previousCode = await SearchCodeAsync(acessCode);
            if (!(previousCode is null))
            {
                await DeleteAsync(previousCode);
            }
            await codigoRepos.AddAsync(acessCode);
        }
        public bool CodeIsValid(string KeyCrip) => Base64Cipher.IsBase64String(KeyCrip);
        public async Task CreateNewKeyAsync(Usuario entity, string codeType)
        {
            var Key = KeyGenerator.GetUniqueKey(8);
            var keyCrip = Base64Cipher.Base64Encode(Key);
            AccessCode newCode = new AccessCode
            {
                Key = Key,
                Email = entity.Email,
                CodeType = codeType,
                DataGerada = DateTime.Now
            };
            if (codeType == CodeType.Verification)
            {
                await emailSvc.SendEmailVerificationAsync(entity, keyCrip);
            }
            else if (codeType == CodeType.Recovery)
            {
                await emailSvc.SendEmailRecoveryAsync(entity, keyCrip);
            }
            await AddAsync(newCode);
        }

        public async Task DeleteAsync(AccessCode acessCode) => await codigoRepos.DeleteAsync(acessCode);

        public async Task<TimeSpan> ElapsedTimeAsync(AccessCode acessCode)
        {
            var previousCodigo = await SearchCodeAsync(acessCode);
            if (!(previousCodigo is null)) 
            {               
                return DateTime.Now.Subtract(previousCodigo.DataGerada);
            }
            return new TimeSpan(0, 15, 0);
        }

        public async Task<AccessCode> SearchAndValidateCodeAsync(AccessCode entity) => await codigoRepos.SearchAndValidateCodeAsync(entity);

        public async Task<AccessCode> SearchCodeAsync(AccessCode entity) => await codigoRepos.SearchCodeAsync(entity);
    }
}
