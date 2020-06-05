using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Models.ViewModel;
using OdontoApp.Services.Interfaces;

namespace OdontoApp.Controllers
{
    [UserAuthorization]
    [AutoValidateAntiforgeryToken]
    public class AnamnesesController : Controller
    {
        private readonly IAnamneseService anamneseSvc;
        private readonly ITipoPerguntaService tipoPerguntaeSvc;
        private readonly IPerguntaAnamneseService perguntaAnamneseSvc;

        public AnamnesesController(IAnamneseService anamneseSvc, ITipoPerguntaService tipoPerguntaeSvc, IPerguntaAnamneseService perguntaAnamneseSvc)
        {
            this.anamneseSvc = anamneseSvc;
            this.tipoPerguntaeSvc = tipoPerguntaeSvc;
            this.perguntaAnamneseSvc = perguntaAnamneseSvc;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListAnamneses(AppView appview)
        {
            return PartialView("_listaanamneses", await anamneseSvc.GetAllAsync(appview));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new AnamneseFormViewModel
            {
                Perguntas = await perguntaAnamneseSvc.GetAllAsync()
            };
            return PartialView("_create", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Anamnese anamneses, List<int> listId)
        {

            if (listId.Count == 0)
            {
                TempData["modal_create"] = true;
                TempData["span"] = "Não foi possível prosseguir, informe nome e perguntas";
                return RedirectToAction(nameof(Index));
            }
            await anamneseSvc.AddAsync(anamneses, listId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            var obj = await anamneseSvc.GetByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return PartialView("_delete", obj);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await anamneseSvc.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await anamneseSvc.GetByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return PartialView("_details", obj);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await anamneseSvc.GetByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            var viewModel = new AnamneseFormViewModel
            {
                Anamnese = obj,
                Perguntas = await perguntaAnamneseSvc.GetAllAsync()
            };

            return PartialView("_edit", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Anamnese anamneses, List<int> listId)
        {

            if (id != anamneses.AnamneseId)
            {
                return NotFound();
            }

            if (listId.Count == 0)
            {
                TempData["modal_edit"] = id;
                TempData["span"] = "Não foi possível prosseguir, informe nome e perguntas";
                return RedirectToAction(nameof(Index));
            }

            await anamneseSvc.UpdateAsync(anamneses, listId);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> PreencherCheckBox(int? id)
        {
            List<int> jsondata = new List<int>();
            if (!(id is null))
            {
                jsondata = await anamneseSvc.CheckBoxChecked(id.Value);
            }
            return Json(jsondata, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None });
        }
    }
}