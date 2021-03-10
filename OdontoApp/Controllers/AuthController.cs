using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OdontoApp.Libraries.Lang;
using OdontoApp.Models;
using OdontoApp.Models.Const;
using OdontoApp.Models.DTO;
using OdontoApp.Models.Helpers;
using OdontoApp.Services.Interfaces;

namespace OdontoApp.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly AppSettings appSettings;
        private readonly IAuthService authService;
        private readonly IKeyService codeSvc;
        private readonly IConfiguration conf;
        private readonly IUsuarioService userService;

        public AuthController(IAuthService authService, IKeyService codeSvc, IConfiguration conf, IOptions<AppSettings> appSettings, IUsuarioService userService)
        {
            this.appSettings = appSettings.Value;
            this.authService = authService;
            this.codeSvc = codeSvc;
            this.conf = conf;
            this.userService = userService;
        }

        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            if (authService.GetLoggedUserAsync().Result is null)
            {
                return View();
            }
            return RedirectToAction("Index", "Agendas");
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromForm]SignInUser signInUser)
        {
            if(!ModelState.IsValid)
            {
                return View(signInUser);
            }
            var user = await userService.FindUserByLoginAsync(signInUser);
            if (user != null)
            {
                await authService.SignInAsync(user);
                return RedirectToAction("Index", "Agendas");
            }
            else
            {
                ViewData["MSG_E"] = Mensagem.MSG_E007;
                return View();
            }
        }

        [HttpGet, Route("signup")]
        public IActionResult SignUp()
        {
            if (authService.GetLoggedUserAsync().Result is null)
            {
                return View();
            }
            return RedirectToAction("Index", "Agendas");
        }

        [HttpPost, Route("signup")]
        public async Task<IActionResult> SignUp([FromForm] SignUpUser signUpUser)
        {
            if (ModelState.IsValid)
            {
                var appUser = new ApplicationUser
                {
                    UserName = signUpUser.Email,
                    PhoneNumber = signUpUser.DDD + signUpUser.Telefone,
                    Nome = signUpUser.Nome,
                    Nascimento = signUpUser.Nascimento,
                    Sexo = signUpUser.Sexo,
                    CPF = signUpUser.CPF,
                    Email = signUpUser.Email,
                    Endereco = signUpUser.Endereco
                };
                var response = await userService.CreateAsync(appUser, signUpUser.Senha);

                if (response != null)
                {
                    ViewData["Response"] = response;
                    return View(signUpUser);
                }
                TempData["MSG_S"] = Mensagem.MSG_S006;
                if (!(signUpUser.Email.ToLower() == appSettings.SmtpUser.ToLower()))
                {
                    await codeSvc.CreateNewKeyAsync(appUser, KeyType.Verification);
                }
                return RedirectToAction(nameof(SignIn));
            }
            return View(signUpUser);
        }

        [Authorize, HttpGet, Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            await authService.SignOutAsync();
            return RedirectToAction("index","usuarios");
        }

    }
}