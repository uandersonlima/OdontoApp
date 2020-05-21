using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Libraries.Seguranca;
using OdontoApp.Models.CodigoAcesso;
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

        public async Task AddAsync(CodigoAcesso accessCode)
        {
            await context.CodigosAcesso.AddAsync(accessCode);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CodigoAcesso accessCode)
        {
            context.CodigosAcesso.Remove(accessCode);
            await context.SaveChangesAsync();
        }
        public async Task<CodigoAcesso> SearchAndValidateCodeAsync(CodigoAcesso accessCode)
        {
            return await context.CodigosAcesso.AsNoTracking().
                        Where(cod => cod.Email.ToLower() == accessCode.Email.ToLower()
                        && cod.CodAcesso == Base64Cipher.Base64Decode(accessCode.CodAcesso)
                        && cod.TipoCodigo == accessCode.TipoCodigo).FirstOrDefaultAsync();
        }
        public async Task<CodigoAcesso> SearchCodeAsync(CodigoAcesso accessCode)
        {
            return await context.CodigosAcesso.AsNoTracking().
                        Where(p => p.TipoCodigo == accessCode.TipoCodigo
                        && p.Email.ToLower() == accessCode.Email.ToLower()).FirstOrDefaultAsync();
        }

    }
}
