using Microsoft.AspNetCore.Identity;
using OdontoApp.Models;
using OdontoApp.Models.Access;
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
        Task<bool> ActiveAccountAsync(ApplicationUser entity, AccessKey accessCode);
        Task<List<string>> CreateAsync(ApplicationUser appUser, string password);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser appUser, string currentPassword, string newPassword);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser appUser, string token, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser appUser);
        Task EnableOrDisableAsync(ApplicationUser entity);
        Task<ApplicationUser> FindUserByLoginAsync(SignInUser signInUser);
        Task<bool> ValidateEmailAsync(string email);
        Task<int> NumberOfUserWithADM();
        int NumberOfUserWithoutADM();
        Task UpdateProfileAsync(ApplicationUser entity);
    }
}
