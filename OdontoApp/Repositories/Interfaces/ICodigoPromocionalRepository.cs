using OdontoApp.Models.Helpers;
using OdontoApp.Models.Promocoes;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface ICodigoPromocionalRepository
    {
        Task AddAsync(CodigoPromocional entity);
        Task<bool> CheckEntityAsync(CodigoPromocional entity);
        Task DeleteAsync(CodigoPromocional entity);
        Task<CodigoPromocional> GetByIdAsync(int id);
        Task<CodigoPromocional> GetByPlanAsync(string planCode);
        Task<PaginationList<CodigoPromocional>> GetAllAsync(AppView appQuery);
        Task UpdateAsync(CodigoPromocional entity);
    }
}
