using OdontoApp.Models;

namespace OdontoApp.Services.Interfaces
{
    public interface ILoginService
    {
        void Login(Usuario user);
        Usuario GetUser();
        void Logout();
    }
}
