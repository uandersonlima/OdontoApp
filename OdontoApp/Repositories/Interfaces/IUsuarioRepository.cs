using OdontoApp.Models;
using OdontoApp.Models.DTO;
using OdontoApp.Models.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AddAsync(ApplicationUser entity);
        Task<bool> CheckEntityAsync(ApplicationUser entity);
        Task<List<string>> CreateAsync(ApplicationUser appUser, string password);
        Task DeleteAsync(ApplicationUser entity);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<PaginationList<ApplicationUser>> GetAllAsync(AppView appQuery);
        Task UpdateAsync(ApplicationUser entity);
        Task ChangePasswordAsync(ApplicationUser entity);
        Task ResetPasswordAsync(ApplicationUser appUser, string token, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser appUser);
        Task<ApplicationUser> FindUserByLoginAsync(SignInUser signInUser);
        Task<bool> ValidateEmailAsync(string email);
        Task<int> NumberOfUserWithADM();
        int NumberOfUserWithoutADM();
        Task UpdateProfileAsync(ApplicationUser entity);
    }
}
