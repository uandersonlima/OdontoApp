using OdontoApp.Models;
using OdontoApp.Models.CodigoAcesso;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Task<bool> ActiveAccountAsync(Usuario entity, CodigoAcesso accessCode);
        Task ChangePasswordAsync(Usuario entity);
        Task<bool> ChangePasswordByCodeAsync(Usuario entity, CodigoAcesso accessCode);
        Task EnableOrDisableAsync(Usuario entity);
        List<Usuario> GetUserByEmail(string email);
        Task<Usuario> GetUserByLogin(string email, string senha);
        Task<int> NumberOfUserWithADM();
        int NumberOfUserWithoutADM();
        Task UpdateProfileAsync(Usuario entity);
    }
}
