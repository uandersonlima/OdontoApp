using OdontoApp.Models;
using OdontoApp.Models.Access;
using System;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IKeyService
    {
        Task AddAsync(AccessKey acessKey);
        bool KeyIsValid(string key);
        Task CreateNewKeyAsync(ApplicationUser entity, string codeType);
        Task DeleteAsync(AccessKey acessKey);
        Task<TimeSpan> ElapsedTimeAsync(AccessKey acessKey);
        Task<AccessKey> SearchKeyAsync(string key);
        Task<AccessKey> SearchKeyByEmailAndTypeAsync(AccessKey acessKey);

    }
}
