using Newtonsoft.Json;
using OdontoApp.Models;
using OdontoApp.Services.Interfaces;

namespace OdontoApp.Services
{
    public class LoginService : ILoginService
    {
        private readonly string Key = "SignIn.User";
        private readonly ISessionService session;

        public LoginService(ISessionService session)
        {
            this.session = session;
        }
        public Usuario GetUser()
        {
            if (session.Check(Key))
            {
                string usuarioJSONString = session.Get(Key);
                var x = JsonConvert.DeserializeObject<Usuario>(usuarioJSONString);
                return x;
            }
            else
            {
                return null;
            }
        }
        public void Login(Usuario user)
        {
            string userJSON = JsonConvert.SerializeObject(user);
            session.Register(Key, userJSON);
        }
        public void Logout()
        {
            session.DeleteAll();
        }
    }
}
