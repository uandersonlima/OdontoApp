using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddAsync(T entity);
        Task<bool> CheckEntityAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int entityId, int userId);
        Task<PaginationList<T>> GetAllAsync(AppView appQuery, int userId);
        Task UpdateAsync(T entity);
    }
}
