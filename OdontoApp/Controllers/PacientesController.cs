using Microsoft.AspNetCore.Mvc;
using OdontoApp.Data;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    [AutoValidateAntiforgeryToken, UserAuthorization]
    public class PacientesController : Controller
    {
        private readonly IAuthService authService;
        private readonly IPacienteService pacienteSvc;
        private readonly IPlanoService planoSvc;
        private readonly OdontoAppContext context;

        public PacientesController(IAuthService authService, IPacienteService pacienteSvc, IPlanoService planoSvc, OdontoAppContext context)
        {
            this.authService = authService;
            this.pacienteSvc = pacienteSvc;
            this.planoSvc = planoSvc;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaPacientes(AppView appview)
        {
            return PartialView("_listapacientes", await pacienteSvc.GetAllAsync(appview));
        }

        [HttpGet]
        public async Task<IActionResult> GerarPacientes()
        {
            var usuario = await authService.GetLoggedUserAsync();
            var list = new List<Paciente>();
            for (int i = 0; i < 1000; i++)
            {
                var pacienteAux = new Paciente
                {
                    Sexo = "M",
                    Nascimento = DateTime.Now,
                    RgPaciente = $"{i}",
                    ObsPaciente = "teste",
                    ComoChegouPaciente = "teste",
                    NumeroProntuarioPaciente = "teste",
                    NomePaciente = $"uandersonLima{i}",
                    CPF = $"{i}{i}-{i}",
                    EmailPaciente = $"uandersonlima{i}@gmail.com",
                    DDD = "71",
                    Telefone = "997788445",
                    EnderecoId = usuario.EnderecoId,
                    UsuarioId = usuario.Id
                };
                list.Add(pacienteAux);
            }
            await context.Paciente.AddRangeAsync(list);
            await context.SaveChangesAsync();
            return Redirect(nameof(Index));
        }


        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (string.IsNullOrEmpty(paciente.Plano.NomePlano) ||
                string.IsNullOrEmpty(paciente.Plano.CpfResponsavelPlano) ||
                string.IsNullOrEmpty(paciente.Plano.NumeroPlano))
            {
                paciente.Plano = null;
            }

            if (!ModelState.IsValid)
            {
                TempData["MSG_E"] = "Informe informações válidas";
                return RedirectToAction(nameof(Index));
            }

            await pacienteSvc.AddAsync(paciente);
            ViewData["MSG_S"] = "Salvo com sucesso";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await pacienteSvc.GetByIdAsync(id.Value);

            if (paciente == null)
            {
                return NotFound();
            }

            return PartialView("_edit", paciente);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Paciente paciente, int? antigoPlano)
        {
            ModelState.Remove("paciente.PlanoId");
            ModelState.Remove("paciente.Plano");
            ModelState.Remove("paciente.Plano.Id");
            ModelState.Remove("paciente.Plano.ClienteId");
            ModelState.Remove("antigoPlano");

            if (!ModelState.IsValid)
            {
                TempData["Edit"] = id;
                TempData["MSG_E"] = "Informe informações válidas";
                return RedirectToAction("PaginaPaciente", new { id });
            }

            if (string.IsNullOrEmpty(paciente.Plano.NomePlano) ||
                string.IsNullOrEmpty(paciente.Plano.CpfResponsavelPlano) ||   //Esse código aqui é para saber se o plano tem outra pessoa, caso contrário remove.
                string.IsNullOrEmpty(paciente.Plano.NumeroPlano))
            {
                paciente.PlanoId = null;
                paciente.Plano = null;
            }

            if (id != paciente.PacienteId)
            {
                return NotFound();
            }

            await pacienteSvc.UpdateAsync(paciente);

            if (antigoPlano.HasValue)
            {
                var plano = await planoSvc.GetByIdAsync(antigoPlano.Value);
                if (plano != null)
                {
                    if (plano.Pacientes.Count is 0)
                    {
                        await planoSvc.DeleteAsync(plano);
                    }
                }
            }

            return RedirectToAction("PaginaPaciente", new { id });

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await pacienteSvc.GetByIdAsync(id.Value);

            if (paciente == null)
            {
                return NotFound();
            }

            return PartialView("_delete", paciente);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));
            await pacienteSvc.DeleteAsync(id.Value);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> PaginaPaciente(int? id)
        {
            if (id == null) return NotFound();
            var paciente = await pacienteSvc.GetByIdAsync(id.Value);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        [HttpGet]
        public async Task<IActionResult> Sobre(int pacienteId)
        {
            return PartialView("_sobre", await pacienteSvc.GetByIdAsync(pacienteId));
        }
    }
}