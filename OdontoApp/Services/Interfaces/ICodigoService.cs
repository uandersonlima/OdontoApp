using OdontoApp.Models;
using OdontoApp.Models.CodigoAcesso;
using System;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface ICodigoService
    {
        Task AddAsync(CodigoAcesso acessCode);
        bool CodeIsValid(string KeyCrip);
        Task CreateNewKeyAsync(Usuario entity, string codeType);
        Task DeleteAsync(CodigoAcesso acessCode);
        Task<TimeSpan> ElapsedTimeAsync(CodigoAcesso acessCode);
        Task<CodigoAcesso> SearchCodeAsync(CodigoAcesso entity);
        Task<CodigoAcesso> SearchAndValidateCodeAsync(CodigoAcesso entity);

    }
}
