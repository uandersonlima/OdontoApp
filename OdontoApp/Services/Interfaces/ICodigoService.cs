using OdontoApp.Models;
using OdontoApp.Models.AccessCode;
using System;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface ICodigoService
    {
        Task AddAsync(AccessCode acessCode);
        bool CodeIsValid(string KeyCrip);
        Task CreateNewKeyAsync(Usuario entity, string codeType);
        Task DeleteAsync(AccessCode acessCode);
        Task<TimeSpan> ElapsedTimeAsync(AccessCode acessCode);
        Task<AccessCode> SearchCodeAsync(AccessCode entity);
        Task<AccessCode> SearchAndValidateCodeAsync(AccessCode entity);

    }
}
