using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Services.Interfaces;

namespace OdontoApp.Controllers
{
    [UserAuthorization]
    [AutoValidateAntiforgeryToken]
    public class MedicosController : Controller
    {
        private readonly IMedicoService medicoSvc;

        public MedicosController(IMedicoService medicoSvc)
        {
            this.medicoSvc = medicoSvc;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> ListaMedicos(AppView appview)
        {
            return PartialView("_listamedicos", await medicoSvc.GetAllAsync(appview));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                NotFound();
            }

            var medico = await medicoSvc.GetByIdAsync(id.Value);

            if (medico == null)
            {
                NotFound();
            }

            return PartialView("_details", medico);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_create");
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,NomeMedico,NumeroCroMedico")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                await medicoSvc.AddAsync(medico);
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await medicoSvc.GetByIdAsync(id.Value);

            if (medico == null)
            {
                return NotFound();
            }
            return PartialView("_edit", medico);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeMedico,NumeroCroMedico")] Medico medico)
        {
            if (id != medico.MedicoId)
            {
                NotFound();
            }

            if (ModelState.IsValid)
            {

                await medicoSvc.UpdateAsync(medico);
                return RedirectToAction(nameof(Index));

            }

            return View(medico);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await medicoSvc.GetByIdAsync(id.Value);

            if (medico == null)
            {
                NotFound();
            }
            return PartialView("_delete", medico);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await medicoSvc.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}