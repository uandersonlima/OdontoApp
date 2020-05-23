using OdontoApp.Models.AccessCode;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface ICodigoRepository
    {
        Task AddAsync(AccessCode accessCode);
        Task DeleteAsync(AccessCode accessCode);
        Task<AccessCode> SearchCodeAsync(AccessCode accessCode);
        Task<AccessCode> SearchAndValidateCodeAsync(AccessCode accessCode);
        
    }
}
