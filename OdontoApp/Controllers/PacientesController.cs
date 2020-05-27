using Microsoft.AspNetCore.Mvc;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    public class PacientesController : Controller
    {
        private readonly ILoginService loginSvc;
        private readonly IPacienteService pacienteSvc;
        private readonly OdontoAppContext context;

        public PacientesController(ILoginService loginSvc, IPacienteService pacienteSvc, OdontoAppContext context)
        {
            this.loginSvc = loginSvc;
            this.pacienteSvc = pacienteSvc;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ListaPacientes(AppView appview)
        {
            return PartialView(await pacienteSvc.GetAllAsync(appview));
        }
        public async Task<IActionResult> GerarPacientes()
        {
            var usuario = loginSvc.GetUser();
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
                    UsuarioId = usuario.UsuarioId
                };
                list.Add(pacienteAux);
            }
            await context.Paciente.AddRangeAsync(list);
            await context.SaveChangesAsync();
            return Redirect(nameof(Index));
        }
        public async Task<IActionResult> Create()
        {
            return PartialView();
        }
    }
}