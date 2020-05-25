using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    //[UserAuthorization]
    [AutoValidateAntiforgeryToken]
    public class AgendasController : Controller
    {
        private readonly IAgendaService agendaSvc;
        private readonly IPacienteService pacienteSvc;

        public AgendasController(IAgendaService agendaSvc, IPacienteService pacienteSvc)
        {
            this.agendaSvc = agendaSvc;
            this.pacienteSvc = pacienteSvc;
        }
        public async Task<IActionResult> Index()
        {
            //ViewData["PacienteId"] = new SelectList(await pacienteSvc.GetAllAsync(), "Id", "NomePaciente");
            return View();
        }

        public async Task<IActionResult> GetEvents(AppView appQuery)
        {
            return Json(await agendaSvc.GetAllAsync(appQuery), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include, Formatting = Formatting.None });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromForm]Agenda agenda)
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
        public async Task<IActionResult> AddEvent([FromForm]Agenda agenda)
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
    }
}