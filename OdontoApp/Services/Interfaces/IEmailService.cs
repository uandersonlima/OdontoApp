using OdontoApp.Models;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(Usuario usuario, string code_encrypted);
        Task SendEmailRecoveryAsync(Usuario usuario, string code_encrypted);

    }
}
