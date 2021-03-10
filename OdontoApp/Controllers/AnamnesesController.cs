using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OdontoApp.Data;
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
        private readonly IPacienteService pacienteSvc;
        private readonly OdontoAppContext context;

        public AnamnesesController(IAnamneseService anamneseSvc, ITipoPerguntaService tipoPerguntaeSvc, IPerguntaAnamneseService perguntaAnamneseSvc, IPacienteService pacienteSvc, OdontoAppContext context)
        {
            this.anamneseSvc = anamneseSvc;
            this.tipoPerguntaeSvc = tipoPerguntaeSvc;
            this.perguntaAnamneseSvc = perguntaAnamneseSvc;
            this.pacienteSvc = pacienteSvc;
            this.context = context;
        }

        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> AutoComplete(string query)
        {
            try
            {
                var anamneses = anamneseSvc.GetAllAsync(new AppView { Search = query }).Result.Select(x => new { x.DescricaoAnamnese, x.AnamneseId });
                return Ok(anamneses);
            }
            catch
            {
                return BadRequest();
            }
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

        #region Tarefas entre Anamnese e Paciente
        public async Task<IActionResult> PacienteAnamneses(int pacienteId, AppView appview)
        {
            ViewBag.PacienteId = pacienteId;
            var anamneses = await anamneseSvc.GetByPatientIdAsync(pacienteId, appview);
            return PartialView("_pacienteanamneses", anamneses);
        }
        [HttpGet]
        public async Task<IActionResult> PacienteAnamnese(int? pacienteId)
        {
            if (pacienteId == null)
            {
                return NotFound();
            }
            var paciente = await pacienteSvc.GetByIdAsync(pacienteId.Value);

            if (paciente == null)
            {
                return NotFound();
            }
            return PartialView("_PacienteAnamnese", paciente);
        }
        [HttpGet]
        public async Task<IActionResult> AddAnamneseToPaciente(int? pacienteId, int? anamneseId)
        {
            if (pacienteId == null || anamneseId == null)
            {
                return NotFound();
            }
            var paciente = await pacienteSvc.GetByIdAsync(pacienteId.Value);
            var anamnese = await anamneseSvc.GetByIdAsync(anamneseId.Value);
            if (paciente == null || anamnese == null)
            {
                return NotFound();
            }
            paciente.Anamneses = new List<Anamnese> { anamnese };
            return PartialView("_AddAnamneseToPaciente", paciente);
        }
        [HttpPost]
        public async Task<IActionResult> AddAnamneseToPaciente(int pacienteId, Paciente paciente)
        {
            try
            {
                var anamnese = paciente.Anamneses.FirstOrDefault();

                anamnese.AnamnesesPerguntas.ForEach(AP =>
                {
                    if (AP.PerguntaAnamnese.TipoPerguntaId == 2)
                    {
                        AP.PerguntaAnamnese.Resposta.Descricao1 += "," + AP.PerguntaAnamnese.Resposta.Descricao2;
                    }
                });
                await anamneseSvc.AddAnamneseToPacienteAsync(anamnese, pacienteId);
                return RedirectToAction("paginapaciente", "pacientes", new { id = pacienteId });
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditAnamneseFromPaciente(int? anamneseId)
        {
            var anm = await anamneseSvc.GetByIdAsync(anamneseId.Value);
            if (anm is null)
            {
                return NotFound();
            }
            anm.AnamnesesPerguntas.ForEach(AP =>
            {
                if (AP.PerguntaAnamnese.TipoPerguntaId == 2)
                {
                    var x = AP.PerguntaAnamnese.Resposta.Descricao1.Split(",");
                    AP.PerguntaAnamnese.Resposta.Descricao1 = x[0];
                    AP.PerguntaAnamnese.Resposta.Descricao2 = x[1];
                }
            });
            return PartialView("_EditAnamneseFromPaciente", anm);
        }
        [HttpPost]
        public async Task<IActionResult> EditAnamneseFromPaciente(Anamnese anamnese)
        {
            try
            {
                anamnese.AnamnesesPerguntas.ForEach(AP =>
                {
                    if (AP.PerguntaAnamnese.TipoPerguntaId == 2)
                    {
                        AP.PerguntaAnamnese.Resposta.Descricao1 += "," + AP.PerguntaAnamnese.Resposta.Descricao2;
                    }
                });
                await anamneseSvc.UpdatePacienteAnamneseAsync(anamnese);
                return RedirectToAction("paginapaciente", "pacientes", new { id = anamnese.PacienteId });
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public async Task<IActionResult> RemoveAnamneseFromPaciente(int? anamneseId)
        {
            if (anamneseId is null)
            {
                return NotFound();
            }
            var anmnese = await anamneseSvc.GetByIdAsync(anamneseId.Value);
            if (anmnese is null) return NotFound();

            return PartialView("_RemoveAnamneseFromPaciente", anmnese);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveAnamneseFromPaciente(int pacienteId, int anamneseId)
        {
            await anamneseSvc.ExcludePacienteAnamneseAsync(pacienteId, anamneseId);
            return RedirectToAction("paginapaciente", "pacientes", new { id = pacienteId });
        }

        public async Task<IActionResult> VisualizarImprimir(int? anamneseId)
        {
            var anm = await anamneseSvc.GetByIdAsync(anamneseId.Value);
            if (anm is null)
            {
                return NotFound();
            }
            anm.AnamnesesPerguntas.ForEach(AP =>
            {
                if (AP.PerguntaAnamnese.TipoPerguntaId == 2)
                {
                    var x = AP.PerguntaAnamnese.Resposta.Descricao1.Split(",");
                    AP.PerguntaAnamnese.Resposta.Descricao1 = x[0];
                    AP.PerguntaAnamnese.Resposta.Descricao2 = x[1];
                }
            });
            return PartialView("_VisualizarImprimir", anm);
        }
        #endregion

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}