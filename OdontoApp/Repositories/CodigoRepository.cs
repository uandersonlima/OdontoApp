using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Libraries.Seguranca;
using OdontoApp.Models.AccessCode;
using OdontoApp.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class CodigoRepository : ICodigoRepository
    {
        private readonly OdontoAppContext context;

        public CodigoRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(AccessCode accessCode)
        {
            await context.AccessCodes.AddAsync(accessCode);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AccessCode accessCode)
        {
            context.AccessCodes.Remove(accessCode);
            await context.SaveChangesAsync();
        }
        public async Task<AccessCode> SearchAndValidateCodeAsync(AccessCode accessCode)
        {
            return await context.AccessCodes.AsNoTracking().
                        Where(cod => cod.Email.ToLower() == accessCode.Email.ToLower()
                        && cod.Key == Base64Cipher.Base64Decode(accessCode.Key)
                        && cod.CodeType == accessCode.CodeType).FirstOrDefaultAsync();
        }
        public async Task<AccessCode> SearchCodeAsync(AccessCode accessCode)
        {
            return await context.AccessCodes.AsNoTracking().
                        Where(p => p.CodeType == accessCode.CodeType
                        && p.Email.ToLower() == accessCode.Email.ToLower()).FirstOrDefaultAsync();
        }

    }
}
