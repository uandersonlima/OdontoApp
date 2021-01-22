using System.Threading.Tasks;
using OdontoApp.Models;

namespace OdontoApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task SignInAsync(ApplicationUser user);
        Task<ApplicationUser> GetLoggedUserAsync();
        Task SignOutAsync();
    }
}