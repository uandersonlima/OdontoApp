using Newtonsoft.Json;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Services.Interfaces;
using System.Linq;

namespace OdontoApp.Services
{
    public class LoginService : ILoginService
    {
        private readonly OdontoAppContext context;
        private readonly string Key = "SignIn.User";
        private readonly ISessionService session;

        public LoginService(OdontoAppContext context, ISessionService session)
        {
            this.context = context;
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
                return context.Usuario.FirstOrDefault();
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
