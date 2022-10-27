using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OdontoApp.Libraries.Lang;
using OdontoApp.Models;
using OdontoApp.Models.Access;
using OdontoApp.Models.Const;
using OdontoApp.Models.DTO;
using OdontoApp.Models.ViewModel;
using OdontoApp.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IKeyService keySvc;
        private readonly IConfiguration conf;
        private readonly IAuthService authService;
        private readonly IUsuarioService userSvc;

        public UsuariosController(IKeyService keySvc, IConfiguration conf, IAuthService authService, IUsuarioService userSvc)
        {
            this.keySvc = keySvc;
            this.conf = conf;
            this.authService = authService;
            this.userSvc = userSvc;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, Route("Perfil")]
        public async Task<IActionResult> MyProfile()
        {
            return View(await userSvc.GetByIdAsync(authService.GetLoggedUserAsync().Result.Id));
        }

        [HttpPost, Route("Perfil")]
        public async Task<IActionResult> MyProfile(ApplicationUser user)
        {
            ModelState.Remove("AcessType");
            ModelState.Remove("AccountStatus");
            ModelState.Remove("PaymentStatus");
            ModelState.Remove("PlanNumber");
            ModelState.Remove("Senha");
            ModelState.Remove("ConfirmacaoSenha");
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MyProfile", user);
            }
            await userSvc.UpdateProfileAsync(user);
            return RedirectToAction("Index", "Agendas");
        }

        [HttpGet("token/{token}")]
        public async Task<IActionResult> Token(string token)
        {
            var serverKey = await keySvc.SearchKeyAsync(token);

            if (serverKey != null)
            {
                TempData["serverKey"] = JsonConvert.SerializeObject(serverKey);
                if (serverKey.KeyType == KeyType.Verification)
                    return RedirectToAction("EmailConfirmado", "Usuarios");
                if (serverKey.KeyType == KeyType.Recovery)
                    return RedirectToAction("RedefinirSenha", "Usuarios");
            }
            return RedirectToAction("ErrorLink", "Error", new { });
        }

        [HttpGet("usuario/confirmacaopendente")]
        public async Task<IActionResult> ConfirmarEmail()
        {
            var loggedUser = await authService.GetLoggedUserAsync();

            if (loggedUser is null)
            {
                return RedirectToAction("signin", "auth"); ;
            }

            if (!loggedUser.EmailConfirmed)
            {
                return View(loggedUser);
            }
            return RedirectToAction("Index", "Agendas");
        }

        [HttpGet("usuario/confirmaremail")]
        public async Task<IActionResult> EmailConfirmado()
        {
            var serverKey = new AccessKey();
            if (TempData["serverKey"] != null)
                serverKey = JsonConvert.DeserializeObject<AccessKey>(TempData["serverKey"].ToString());

            if (serverKey.Key != null && serverKey.KeyType != null && serverKey.UserId != null)
            {
                var user = await userSvc.GetByIdAsync(serverKey.UserId);
                var accountEnabled = await userSvc.ActiveAccountAsync(user, serverKey);

                if (!accountEnabled)
                {
                    ViewData["MSG_E"] = "Email não pode ser verificado, tente novamente em instantes, se o problema persistir entre em contato conosco.";
                    return View();
                }
                ViewData["MSG_S"] = "Email verificado com sucesso!";
                return View();

            }
            ViewData["MSG_E"] = "Essa chamada não pôde ser processada.";

            return View();
        }

        [HttpGet]
        public IActionResult NovaConfirmacaoEmail() => PartialView("_NovaConfirmacaoEmail");

        [HttpPost]
        public async Task<IActionResult> NovaConfirmacaoEmail(string codeType)
        {   
            var loggedUser = await authService.GetLoggedUserAsync();
            if (!(loggedUser is null))
            {
                var codigo = new AccessKey { User = loggedUser, KeyType = codeType };
                var elapsedTime = await keySvc.ElapsedTimeAsync(codigo);
                var _15min = new TimeSpan(0, 15, 0);
                TempData["MSG_E"] = string.Format(Mensagem.MSG_E011, _15min.Subtract(elapsedTime).ToString(@"mm\:ss"));


                if (elapsedTime >= _15min)
                {
                    TempData["MSG_E"] = null;
                    await keySvc.CreateNewKeyAsync(loggedUser, codeType);
                    TempData["MSG_S"] = Mensagem.MSG_S007;
                }

                return RedirectToAction("ConfirmarEmail");
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecuperarSenha(SignInUser usuario)
        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var user = await userSvc.FindByEmailAsync(usuario.Username);
                if (user == null)
                {
                    TempData["MSG_E"] = Mensagem.MSG_E014;
                    return View();
                }

                var codigo = new AccessKey { User = new ApplicationUser { Email = usuario.Username }, KeyType = KeyType.Recovery };
                var elapsedTime = await keySvc.ElapsedTimeAsync(codigo);
                var _15min = new TimeSpan(0, 15, 0);
                TempData["MSG_E"] = string.Format(Mensagem.MSG_E011, _15min.Subtract(elapsedTime).ToString(@"mm\:ss"));

                if (elapsedTime >= _15min)
                {
                    TempData["MSG_E"] = null;
                    await keySvc.CreateNewKeyAsync(user, KeyType.Recovery);
                    TempData["MSG_S"] = Mensagem.MSG_S003;
                }
            }
            return View();
        }

        [HttpGet/*("usuario/redefinir/{token}")*/]
        public IActionResult RedefinirSenha(/*string token*/)
        {
            var serverKey = new AccessKey();
            if (TempData["serverKey"] != null)
            {
                TempData.Keep("serverKey");
                return View();
            }
            TempData["MSG_E"] = "Link inválido ou expirado, solicite outra redefinição de senha ou entre em contato conosco";
            return RedirectToAction("RecuperarSenha");
        }

        [HttpPost]
        public async Task<IActionResult> RedefinirSenha([FromForm]UserResetPassword userResetPassword)
        {
            var serverKey = new AccessKey();

            if (ModelState.IsValid && TempData["serverKey"] != null)
            {
                serverKey = JsonConvert.DeserializeObject<AccessKey>(TempData["serverKey"].ToString());
                TempData.Remove("serverKey");

                if (serverKey.Key != null && serverKey.KeyType != null && serverKey.UserId != null)
                {
                    var user = await userSvc.GetByIdAsync(serverKey.UserId);
                    if (user != null)
                    {
                        var token = await userSvc.GeneratePasswordResetTokenAsync(user);
                        var response = await userSvc.ResetPasswordAsync(user, token, userResetPassword.Password);
                        if (response.Succeeded)
                        {
                            TempData["MSG_S"] = Mensagem.MSG_S005;
                            await keySvc.DeleteAsync(serverKey);
                            return RedirectToAction("ResultResetPassword");
                        }
                    }

                }
            }
            TempData.Remove("serverKey");
            return RedirectToAction("ResultResetPassword");
        }
        public async Task<IActionResult> ResultResetPassword()
        {
            return View();
        }
    }
}