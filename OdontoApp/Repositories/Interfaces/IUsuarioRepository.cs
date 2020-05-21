using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AddAsync(Usuario entity);
        Task<bool> CheckEntityAsync(Usuario entity);
        Task DeleteAsync(Usuario entity);
        Task<Usuario> GetByIdAsync(int id);
        Task<PaginationList<Usuario>> GetAllAsync(AppQuery appQuery);
        Task UpdateAsync(Usuario entity);
        Task ChangePasswordAsync(Usuario entity);
        List<Usuario> GetUserByEmail(string email);
        Task<Usuario> GetUserByLogin(string email, string senha);
        Task<int> NumberOfUserWithADM();
        int NumberOfUserWithoutADM();
        Task UpdateProfileAsync(Usuario entity);
    }
}
