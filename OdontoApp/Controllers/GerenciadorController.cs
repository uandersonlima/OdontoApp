using Microsoft.AspNetCore.Mvc;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models.Const;
using OdontoApp.Models.Helpers;
using OdontoApp.Models.Promocoes;
using OdontoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    [UserAuthorization(AccessType.Administrator), AutoValidateAntiforgeryToken]
    public class GerenciadorController : Controller
    {
        private readonly IUsuarioService userSvc;
        private readonly ICodigoPromocionalService promoSvc;

        public GerenciadorController(IUsuarioService userSvc, ICodigoPromocionalService promoSvc)
        {
            this.userSvc = userSvc;
            this.promoSvc = promoSvc;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios(AppView appview)
        {
            return PartialView("_listausuarios", await userSvc.GetAllAsync(appview));
        }

        [HttpGet]
        public async Task<IActionResult> DetalhesCliente(int id)
        {
            return PartialView(await userSvc.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> ListarCodigosPromocionais(AppView appview)
        {
            return PartialView("_listacodigospromocionais", await promoSvc.GetAllAsync(appview));
        }

        [HttpGet]
        public IActionResult AddCodigoPromocional()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddCodigoPromocional(CodigoPromocional promo)
        {
            if (ModelState.IsValid)
            {
                await promoSvc.AddAsync(promo);
                RedirectToAction(nameof(Index));
            }
            ViewData["MSG_E"] = "Não foi possível cadastrar o novo código";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> EditCodigoPromocional(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var promo = await promoSvc.GetByIdAsync(id.Value);

            if (promo == null)
            {
                return NotFound();
            }

            return PartialView(promo);
        }

        [HttpPost]
        public async Task<IActionResult> EditCodigoPromocional(int id, CodigoPromocional promo)
        {
            if(id != promo.CodigoPromocionalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await promoSvc.UpdateAsync(promo);
                RedirectToAction(nameof(Index));
            }

            ViewData["MSG_E"] = "Não foi possível atualizar o novo código";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCodigoPromocional(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var promo = await promoSvc.GetByIdAsync(id.Value);

            if (promo == null)
            {
                return NotFound();
            }

            return PartialView(promo);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCodigoPromocional(int id)
        {
            await promoSvc.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }

}