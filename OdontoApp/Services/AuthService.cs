using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OdontoApp.Models;
using OdontoApp.Services.Interfaces;

namespace OdontoApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor contextAccessor;

        public AuthService(SignInManager<ApplicationUser> signInManager, IHttpContextAccessor contextAccessor)
        {
            this.signInManager = signInManager;
            this.contextAccessor = contextAccessor;
        }

        public async Task<ApplicationUser> GetLoggedUserAsync()
        {
            return await signInManager.UserManager.GetUserAsync(contextAccessor.HttpContext.User);
        }

        public async Task SignInAsync(ApplicationUser user)
        {
            await signInManager.SignInAsync(user, false);
        }
        
        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}