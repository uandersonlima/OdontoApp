using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddAsync(T entity);
        Task<bool> CheckEntityAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int entityId, string userId);
        Task<PaginationList<T>> GetAllAsync(AppView appQuery, string userId);
        Task UpdateAsync(T entity);
    }
}
