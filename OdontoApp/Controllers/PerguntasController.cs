using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models;
using OdontoApp.Services.Interfaces;

namespace OdontoApp.Controllers
{
    [AutoValidateAntiforgeryToken, UserAuthorization]
    public class PerguntasController : Controller
    {
        private readonly IPerguntaAnamneseService perguntaAnamneseSvc;
        private readonly ITipoPerguntaService tipoPerguntaSvc;

        public PerguntasController(IPerguntaAnamneseService perguntaAnamneseSvc, ITipoPerguntaService tipoPerguntaSvc)
        {
            this.perguntaAnamneseSvc = perguntaAnamneseSvc;
            this.tipoPerguntaSvc = tipoPerguntaSvc;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? editAdd)
        {
            if (editAdd.HasValue)
            {
                TempData["modal_edit"] = editAdd;
            }
            ViewData["TipoPerguntaId"] = new SelectList(await tipoPerguntaSvc.GetAllAsync(), "TipoPerguntaId", "DescricaoTipoPergunta");
            return PartialView("_create");
        }


        [HttpPost]
        public async Task<IActionResult> Create(PerguntaAnamnese perguntaAnamnese)
        {
            if (ModelState.IsValid)
            {
                await perguntaAnamneseSvc.AddAsync(perguntaAnamnese);
                TempData["modal_create"] = true;
                return RedirectToAction("Index", "Anamneses");
            }
            ViewData["TipoPerguntaId"] = new SelectList(await tipoPerguntaSvc.GetAllAsync(), "TipoPerguntaId", "DescricaoTipoPergunta", perguntaAnamnese.TipoPerguntaId);
            return View(perguntaAnamnese);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, int? editAdd)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perguntaAnamnese = await perguntaAnamneseSvc.GetByIdAsync(id.Value);

            if (perguntaAnamnese == null)
            {
                return NotFound();
            }

            if (editAdd.HasValue)
            {
                TempData["modal_edit"] = editAdd;
            }

            ViewData["TipoPerguntaId"] = new SelectList(await tipoPerguntaSvc.GetAllAsync(), "TipoPerguntaId", "DescricaoTipoPergunta", perguntaAnamnese.TipoPerguntaId);
            
            return PartialView("_edit", perguntaAnamnese);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int PerguntaAnamneseId, PerguntaAnamnese perguntaAnamnese)
        {
            if (PerguntaAnamneseId != perguntaAnamnese.PerguntaAnamneseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await perguntaAnamneseSvc.UpdateAsync(perguntaAnamnese);
                TempData["modal_create"] = true;
                return RedirectToAction("Index", "Anamneses");
            }

            ViewData["TipoPerguntaId"] = new SelectList(await tipoPerguntaSvc.GetAllAsync(), "TipoPerguntaId", "DescricaoTipoPergunta", perguntaAnamnese.TipoPerguntaId);
            return View(perguntaAnamnese);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perguntaAnamnese = await perguntaAnamneseSvc.GetByIdAsync(id.Value);

            if (perguntaAnamnese == null)
            {
                return NotFound();
            }

            return PartialView("_delete", perguntaAnamnese);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await perguntaAnamneseSvc.DeleteAsync(id);
            return RedirectToAction("Index", "Anamneses");
        }

    }
}