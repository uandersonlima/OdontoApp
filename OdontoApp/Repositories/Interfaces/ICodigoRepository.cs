using OdontoApp.Models.CodigoAcesso;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface ICodigoRepository
    {
        Task AddAsync(CodigoAcesso accessCode);
        Task DeleteAsync(CodigoAcesso accessCode);
        Task<CodigoAcesso> SearchCodeAsync(CodigoAcesso accessCode);
        Task<CodigoAcesso> SearchAndValidateCodeAsync(CodigoAcesso accessCode);
        
    }
}
