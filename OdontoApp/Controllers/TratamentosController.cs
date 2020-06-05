using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models;
using OdontoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    [UserAuthorization, AutoValidateAntiforgeryToken]
    public class TratamentosController : Controller
    {
        private readonly ITratamentoService tratamentoSvc;
        private readonly IPacienteService pacienteSvc;
        private readonly IDentesRegiaoService dentesSvc;

        public TratamentosController(ITratamentoService tratamentoSvc, IPacienteService pacienteSvc, IDentesRegiaoService dentesSvc)
        {
            this.tratamentoSvc = tratamentoSvc;
            this.pacienteSvc = pacienteSvc;
            this.dentesSvc = dentesSvc;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? pacienteId)
        {
            if (!pacienteId.HasValue)
            {
                return NotFound();
            }

            var paciente = await pacienteSvc.GetByIdAsync(pacienteId.Value);

            if (paciente == null)
            {
                return NotFound();

            }
            TempData["aba"] = 3;
            return View(paciente);
        }

        [HttpGet]
        public IActionResult TratamentosDoPaciente()
        {
            return PartialView();
        }
        public async Task<IActionResult> AdicionarTratamentosAoPaciente(int pacienteId)
        {

            var paciente = await pacienteSvc.GetByIdAsync(pacienteId); 

            if (paciente == null)
            {
                return NotFound();
            }

            Tratamento tratamento = new Tratamento
            {
                PacienteId = paciente.PacienteId
            };

            if (paciente.PlanoId != null)
            {
                tratamento.PlanoId = paciente.PlanoId.Value;
            }

            ViewData["NomePaciente"] = $"{paciente.NomePaciente}";
            ViewData["DentesRegiaoId"] = new SelectList(await dentesSvc.GetAllAsync(), "Id", "Descricao");
            return PartialView(tratamento);

        }
        public IActionResult AdicionarTratamentosAoPaciente(Tratamento tratamento)
        {
            return PartialView();
        }
        public IActionResult ListarTratamentosDoPaciente()
        {
            return PartialView();
        }
        public IActionResult EditarTratamentosDoPaciente()
        {
            return PartialView();
        }
        public IActionResult ApagarTratamentosDoPaciente()
        {
            return PartialView();
        }

    }
}