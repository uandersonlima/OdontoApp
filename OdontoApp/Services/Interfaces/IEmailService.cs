using OdontoApp.Models;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(ApplicationUser usuario, string key);
        Task SendEmailRecoveryAsync(ApplicationUser usuario, string key);

    }
}
