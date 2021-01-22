using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OdontoApp.Libraries.Lang;
using OdontoApp.Models;
using OdontoApp.Models.AccessCode;
using OdontoApp.Models.Const;
using OdontoApp.Models.ViewModel;
using OdontoApp.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ICodigoService codeSvc;
        private readonly IConfiguration conf;
        private readonly IAuthService authService;
        private readonly IUsuarioService userSvc;

        public UsuariosController(ICodigoService codeSvc, IConfiguration conf, IAuthService authService, IUsuarioService userSvc)
        {
            this.codeSvc = codeSvc;
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

        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecuperarSenha(ApplicationUser usuario)
        {
            var user = await userSvc.FindByEmailAsync(usuario.Email);
            if (user == null)
            {
                TempData["MSG_E"] = Mensagem.MSG_E014;
                return View();
            }
            var codigo = new AccessCode { Email = usuario.Email, CodeType = CodeType.Recovery };
            var elapsedTime = await codeSvc.ElapsedTimeAsync(codigo);
            var _15min = new TimeSpan(0, 15, 0);
            TempData["MSG_E"] = string.Format(Mensagem.MSG_E011, _15min.Subtract(elapsedTime).ToString(@"mm\:ss"));

            if (elapsedTime >= _15min)
            {
                TempData["MSG_E"] = null;
                await codeSvc.CreateNewKeyAsync(user, CodeType.Recovery);
                TempData["MSG_S"] = Mensagem.MSG_S003;
            }
            usuario.Id = user.Id;
            return RedirectToAction("NovaSenha", usuario);
        }

        [HttpGet]
        public IActionResult NovaSenha(ApplicationUser usuario)
        {
            UserCode viewModel = new UserCode
            {
                Usuario = usuario
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> NovaSenha(UserCode usercode)
        {
            ModelState.Remove("Usuario.Nome");
            ModelState.Remove("Usuario.TipoAcesso");
            ModelState.Remove("Usuario.SituacaoConta");
            ModelState.Remove("Usuario.SituacaoPagamento");
            ModelState.Remove("Usuario.NumeroPlano");
            ModelState.Remove("Usuario.Nascimento");
            ModelState.Remove("Usuario.Sexo");
            ModelState.Remove("Usuario.CPF");
            ModelState.Remove("Usuario.DDD");
            ModelState.Remove("Usuario.EnderecoId");
            ModelState.Remove("Usuario.Telefone");


            if (ModelState.IsValid)
            {
                usercode.AcessCode.Email = usercode.Usuario.Email;
                usercode.AcessCode.CodeType = CodeType.Recovery;

                if (codeSvc.CodeIsValid(usercode.AcessCode.Key))
                {

                    if (!await userSvc.ChangePasswordByCodeAsync(usercode.Usuario, usercode.AcessCode))
                    {
                        TempData["MSG_E"] = Mensagem.MSG_E015;
                        usercode.AcessCode.Key = string.Empty;
                        return View(usercode);
                    }

                    TempData["MSG_S"] = Mensagem.MSG_S005;
                    return RedirectToAction("Login","Usuarios");
                }
            }

            TempData["MSG_E"] = Mensagem.MSG_E013;
            return View(usercode);
        }

        [HttpGet]
        public async Task<IActionResult> AtivarConta()
        {
            var loggedUser = await authService.GetLoggedUserAsync();

            if (loggedUser is null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            if (!loggedUser.EmailConfirmed)
            {
                UserCode viewModel = new UserCode
                {
                    Usuario = loggedUser
                };

                return View(viewModel);

            }
            return RedirectToAction("Index", "Agendas");
        }

        [HttpPost]
        public async Task<IActionResult> AtivarConta(UserCode usercode)
        {
            var loggedUser = await authService.GetLoggedUserAsync();

            if (!loggedUser.EmailConfirmed)
            {
                ModelState.Remove("Usuario.Nome");
                ModelState.Remove("Usuario.TipoAcesso");
                ModelState.Remove("Usuario.SituacaoConta");
                ModelState.Remove("Usuario.SituacaoPagamento");
                ModelState.Remove("Usuario.NumeroPlano");
                ModelState.Remove("Usuario.Nascimento");
                ModelState.Remove("Usuario.Sexo");
                ModelState.Remove("Usuario.CPF");
                ModelState.Remove("Usuario.DDD");
                ModelState.Remove("Usuario.EnderecoId");
                ModelState.Remove("Usuario.Telefone");
                ModelState.Remove("Usuario.Senha");
                ModelState.Remove("Usuario.ConfirmacaoSenha");

                if (ModelState.IsValid)
                {
                    usercode.AcessCode.Email = usercode.Usuario.Email;
                    usercode.AcessCode.CodeType = CodeType.Verification;

                    if (codeSvc.CodeIsValid(usercode.AcessCode.Key))
                    {
                        var accountEnabled = await userSvc.ActiveAccountAsync(loggedUser, usercode.AcessCode);

                        if (!accountEnabled)
                        {
                            TempData["MSG_E"] = Mensagem.MSG_E015;
                            return View(usercode);
                        }

                        await authService.SignInAsync(await userSvc.GetByIdAsync(loggedUser.Id));
                        TempData["MSG_S"] = Mensagem.MSG_S008;
                        return RedirectToAction("Index", "Agendas");
                    }
                }
                TempData["MSG_E"] = Mensagem.MSG_E013;
                return View(usercode);
            }
            return RedirectToAction("Index", "Agendas");
        }

        [HttpGet]
        public IActionResult NovoCodigoConfirmacao() => PartialView();

        [HttpPost]
        public async Task<IActionResult> NovoCodigoConfirmacao(string codeType)
        {
            var loggedUser = await authService.GetLoggedUserAsync();
            if (!(loggedUser is null))
            {
                var codigo = new AccessCode { Email = loggedUser.Email, CodeType = codeType };
                var elapsedTime = await codeSvc.ElapsedTimeAsync(codigo);
                var _15min = new TimeSpan(0, 15, 0);
                TempData["MSG_E"] = string.Format(Mensagem.MSG_E011, _15min.Subtract(elapsedTime).ToString(@"mm\:ss"));


                if (elapsedTime >= _15min)
                {
                    TempData["MSG_E"] = null;
                    await codeSvc.CreateNewKeyAsync(loggedUser, codeType);
                    TempData["MSG_S"] = Mensagem.MSG_S007;
                }

                return RedirectToAction("AtivarConta", new UserCode { Usuario = loggedUser });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}