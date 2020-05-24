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
        private readonly ILoginService loginSvc;
        private readonly IUsuarioService userSvc;

        public UsuariosController(ICodigoService codeSvc, IConfiguration conf, ILoginService loginSvc, IUsuarioService userSvc)
        {
            this.codeSvc = codeSvc;
            this.conf = conf;
            this.loginSvc = loginSvc;
            this.userSvc = userSvc;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (loginSvc.GetUser() is null)
            {
                return View();
            }
            return RedirectToAction("Index", "Agendas");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]Usuario usuario)
        {

            Usuario usuarioDB = await userSvc.GetUserByLogin(usuario.Email, usuario.Senha);

            if (!(usuarioDB is null))
            {
                loginSvc.Login(usuarioDB);
                return RedirectToAction("Index", "Agendas");
            }
            else
            {
                ViewData["MSG_E"] = Mensagem.MSG_E007;
                return View();
            }
        }
        [HttpGet, Route("[controller]/Cadastro")]
        public IActionResult CadastroUsuario()
        {
            if (loginSvc.GetUser() is null)
            {
                return View();
            }
            return RedirectToAction("Index", "Agendas");
        }
        [HttpPost]
        public async Task<IActionResult> CadastroUsuario([FromForm]Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                await userSvc.AddAsync(usuario);
                TempData["MSG_S"] = Mensagem.MSG_S006;
                if (!(usuario.Email.ToLower() == conf.GetValue<string>("Email:Username").ToLower()))
                {
                    await codeSvc.CreateNewKeyAsync(usuario, CodeType.Verification);
                }
                return RedirectToAction(nameof(Login));
            }
            return View(usuario);
        }
        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecuperarSenha(Usuario usuario)
        {
            var usuarios = userSvc.GetUserByEmail(usuario.Email);
            if (usuarios.Count is 0)
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
                await codeSvc.CreateNewKeyAsync(usuarios[0], CodeType.Recovery);
                TempData["MSG_S"] = Mensagem.MSG_S003;
            }

            usuario.UsuarioId = usuarios[0].UsuarioId;
            return RedirectToAction("NovaSenha", usuario);
        }

        [HttpGet]
        public IActionResult NovaSenha(Usuario usuario)
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
        public IActionResult AtivarConta()
        {
            var loggedUser = loginSvc.GetUser();

            if (loggedUser is null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            if (loggedUser.AccountStatus != Status.Enabled)
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
            var loggedUser = loginSvc.GetUser();

            if (loggedUser.AccountStatus != Status.Enabled)
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

                        loginSvc.Login(await userSvc.GetByIdAsync(loggedUser.UsuarioId));
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
            var loggedUser = loginSvc.GetUser();
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

        [HttpGet]
        public IActionResult Logout()
        {
            loginSvc.Logout();
            return RedirectToAction(nameof(Index));
        }
    }
}