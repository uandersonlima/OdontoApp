using OdontoApp.Models;
using OdontoApp.Models.AccessCode;
using OdontoApp.Models.DTO;
using OdontoApp.Models.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IUsuarioService
    {    
        Task AddAsync(ApplicationUser entity);
        Task<bool> CheckEntityAsync(ApplicationUser entity);
        Task DeleteAsync(string userId);
        Task DeleteAsync(ApplicationUser entity);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task UpdateAsync(ApplicationUser entity);
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<List<ApplicationUser>> GetAllAsync();
        Task<PaginationList<ApplicationUser>> GetAllAsync(AppView appQuery);
        Task<bool> ActiveAccountAsync(ApplicationUser entity, AccessCode accessCode);
        Task<List<string>> CreateAsync(ApplicationUser appUser, string password);
        Task ChangePasswordAsync(ApplicationUser entity);
        Task<bool> ChangePasswordByCodeAsync(ApplicationUser entity, AccessCode accessCode);
        Task EnableOrDisableAsync(ApplicationUser entity);
        Task<ApplicationUser> FindUserByLoginAsync(SignInUser signInUser);
        Task<bool> ValidateEmailAsync(string email);
        Task<int> NumberOfUserWithADM();
        int NumberOfUserWithoutADM();
        Task UpdateProfileAsync(ApplicationUser entity);
    }
}
