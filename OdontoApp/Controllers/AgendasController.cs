using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Models.ViewModel;
using OdontoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    [UserAuthorization]
    [AutoValidateAntiforgeryToken]
    public class AgendasController : Controller
    {
        private readonly IAgendaService agendaSvc;
        private readonly IPacienteService pacienteSvc;
        private readonly IMedicoService medicoSvc;

        public AgendasController(IAgendaService agendaSvc, IPacienteService pacienteSvc, IMedicoService medicoSvc)
        {
            this.agendaSvc = agendaSvc;
            this.pacienteSvc = pacienteSvc;
            this.medicoSvc = medicoSvc;
        }

        public async Task<IActionResult> Index()
        {
            var pacientesMedicos = new PacientesMedicos
            {
                Medicos = new SelectList(await medicoSvc.GetAllAsync(), "MedicoId", "NomeMedico"),
                Pacientes = new SelectList(await pacienteSvc.GetAllAsync(),"PacienteId","NomePaciente")
            };
            return View(pacientesMedicos);
        }

        public async Task<IActionResult> GetEvents(AppView appQuery)
        {
            return Json(await agendaSvc.GetAllAsync(appQuery), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include, Formatting = Formatting.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }      

        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromForm] Agenda agenda)
        {
            var status = false;
            if (!(agenda.AgendaId is 0))
            {
                await agendaSvc.UpdateAsync(agenda);
                status = !status;
            }

            return Json(new { status }, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include, Formatting = Formatting.None });
        }


        [HttpPost]
        public async Task<IActionResult> AddEvent([FromForm] Agenda agenda)
        {
            bool status = false;
            if (!(agenda is null))
            {
                await agendaSvc.AddAsync(agenda);
                status = !status;
            }

            return Json(new { status }, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include, Formatting = Formatting.None });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            bool status = false;
            if (!(id is 0))
            {
                await agendaSvc.DeleteAsync(id);
                status = !status;
            }

            return Json(new { status }, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include, Formatting = Formatting.None });
        }

        #region Interações com os Pacientes
        [HttpGet]
        public async Task<IActionResult> PacienteAgendas(int pacienteId, AppView appview)
        {
            var response = await agendaSvc.GetByPatientIdAsync(appview, pacienteId);
            return PartialView("_pacienteAgendas", response);
        }
         #endregion
    }
}