using Microsoft.AspNetCore.Mvc;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models;
using OdontoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    [UserAuthorization]
    [AutoValidateAntiforgeryToken]
    public class ClinicasController : Controller
    {
        private readonly IClinicaService clinicaSvc;

        public ClinicasController(IClinicaService clinicaSvc)
        {
            this.clinicaSvc = clinicaSvc;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Clinica clinica)
        {
            if (!ModelState.IsValid)
            {
                return View(clinica);
            }
            await clinicaSvc.AddAsync(clinica);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var clinica = await clinicaSvc.GetByIdAsync(id.Value);

            if (clinica == null)
            {
                return NotFound();
            }

            return View(clinica);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await clinicaSvc.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var clinica = await clinicaSvc.GetByIdAsync(id.Value);

            if (clinica == null)
            {
                return NotFound();
            }

            return View(clinica);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var clinica = await clinicaSvc.GetByIdAsync(id.Value);

            if (clinica == null)
            {
                return NotFound();
            }

            return View(clinica);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Clinica clinica)
        {
            if (!ModelState.IsValid)
            {
                return View(clinica);
            }
            if (id != clinica.ClinicaId)
            {
                return NotFound();
            }
            await clinicaSvc.UpdateAsync(clinica);
            return RedirectToAction(nameof(Index));

        }
    }
}