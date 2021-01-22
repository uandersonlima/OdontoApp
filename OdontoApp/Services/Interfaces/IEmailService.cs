using OdontoApp.Models;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(ApplicationUser usuario, string code_encrypted);
        Task SendEmailRecoveryAsync(ApplicationUser usuario, string code_encrypted);

    }
}
