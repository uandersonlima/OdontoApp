using OdontoApp.Models.Access;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IKeyRepository
    {
        Task AddAsync(AccessKey accessKey);
        Task DeleteAsync(AccessKey accessKey);
        Task<AccessKey> SearchKeyAsync(string key);
        Task<AccessKey> SearchKeyByEmailAndTypeAsync(AccessKey accessKey);
        
    }
}
