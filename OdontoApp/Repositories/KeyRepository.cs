using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Libraries.Seguranca;
using OdontoApp.Models.Access;
using OdontoApp.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class KeyRepository : IKeyRepository
    {
        private readonly OdontoAppContext context;

        public KeyRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(AccessKey accessKey)
        {
            await context.AccessKey.AddAsync(accessKey);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AccessKey accessKey)
        {
            accessKey.User = null;
            context.AccessKey.Remove(accessKey);
            await context.SaveChangesAsync();
        }
        public async Task<AccessKey> SearchKeyByEmailAndTypeAsync(AccessKey accessKey)
        {
            return await context.AccessKey.AsNoTracking().Include(a => a.User).
                        Where(p => p.KeyType == accessKey.KeyType
                        && p.User.Email.ToLower() == accessKey.User.Email.ToLower()).FirstOrDefaultAsync();
        }
        public async Task<AccessKey> SearchKeyAsync(string key)
        {
            return await context.AccessKey.AsNoTracking()
                        .Where(p => p.Key == key)
                        .FirstOrDefaultAsync();
        }
    }
}
