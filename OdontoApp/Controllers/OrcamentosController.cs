using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Services.Interfaces;

namespace OdontoApp.Controllers
{
    [UserAuthorization]
    [AutoValidateAntiforgeryToken]
    public class OrcamentosController : Controller
    {
        private readonly IOrcamentoService orcamentoSvc;
        private readonly ITratamentoService tratamentoSvc;

        public OrcamentosController(IOrcamentoService orcamentoSvc, ITratamentoService tratamentoSvc)
        {
            this.orcamentoSvc = orcamentoSvc;
            this.tratamentoSvc = tratamentoSvc;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pacienteId, AppView appview)
        {
            TempData["aba"] = 2;
            TempData["pacienteId"] = pacienteId;

            return PartialView(await orcamentoSvc.GetByPatientAsync(appview, pacienteId));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int pacienteId, AppView appview)
        {
            ViewData["Tratamentos"] = await tratamentoSvc.GetByPatientAsync(appview, pacienteId);
            return PartialView(new Orcamento { PacienteId = pacienteId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Orcamento orcamento, List<int> listId)
        {
            if (listId.Count == 0)
            {
                TempData["HasQuestion"] = "tem";
                TempData["span"] = "Algo deu errado, tente novamente";
                return RedirectToAction("PaginaPaciente", "Pacientes", new { id = orcamento.PacienteId });
            }
            await orcamentoSvc.AddAsync(orcamento, listId);
            return RedirectToAction("PaginaPaciente", "Pacientes", new { id = orcamento.PacienteId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await orcamentoSvc.GetByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return PartialView(obj);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, int pacienteId)
        {
            await orcamentoSvc.DeleteAsync(id);
            return RedirectToAction("PaginaPaciente", "Pacientes", new { id = pacienteId });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await orcamentoSvc.GetByIdAsync(id.Value);
            
            if (obj == null)
            {
                return NotFound();
            }

            return PartialView(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await orcamentoSvc.GetByIdAsync(id.Value);
            //ViewData["Tratamentos"] = await tratamentoService.BuscarTratamentos(obj.PacienteId);

            if (obj == null)
            {
                return NotFound();
            }

            return PartialView(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Orcamento orcamento, List<int> listId)
        {
            if (id != orcamento.OrcamentoId)
            {
                return NotFound();
            }

            if (listId.Count == 0)
            {
                TempData["HasEdit"] = id;
                TempData["span"] = "Não foi possível prosseguir, informe nome e perguntas";
                return RedirectToAction(nameof(Index));
            }

            await orcamentoSvc.UpdateAsync(orcamento, listId);

            return RedirectToAction("PaginaPaciente", "Pacientes", new { id = orcamento.PacienteId });

        }

        [HttpGet]
        public async Task<IActionResult> PreencherCheckBox(int? id)
        {
            List<int> jsondata = new List<int>();
            if (!(id is null))
            {
                jsondata = await orcamentoSvc.CheckBoxChecked(id.Value);
            }
            return Json(jsondata, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None });
        }
    }
}