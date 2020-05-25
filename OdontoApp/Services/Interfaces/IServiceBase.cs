using OdontoApp.Models.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IServiceBase<T> where T : class
    {
        Task AddAsync(T entity);
        Task<bool> CheckEntityAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<PaginationList<T>> GetAllAsync(AppView appQuery);
    }
}
