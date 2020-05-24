using Microsoft.AspNetCore.Mvc;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Collections.Generic;

namespace OdontoApp.Controllers
{
    public class PacientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListaPacientes(AppQuery appquery)
        {
            var listPacientes = new List<Paciente> {
                new Paciente
                {
                    NomePaciente = "Uanderson Lima",
                    CPF = "068.296.335-67",
                    EmailPaciente = "uandersonlima@gmail.com",
                    DDD = "71",
                    Telefone = "99667-7884"
                },
                new Paciente
                {
                    NomePaciente = "Uanderson Lima",
                    CPF = "068.296.335-67",
                    EmailPaciente = "uandersonlima@gmail.com",
                    DDD = "71",
                    Telefone = "99667-7884"
                },
                new Paciente
                {
                    NomePaciente = "Uanderson Lima",
                    CPF = "068.296.335-67",
                    EmailPaciente = "uandersonlima@gmail.com",
                    DDD = "71",
                    Telefone = "99667-7884"
                }
                ,
                new Paciente
                {
                    NomePaciente = "Uanderson Lima",
                    CPF = "068.296.335-67",
                    EmailPaciente = "uandersonlima@gmail.com",
                    DDD = "71",
                    Telefone = "99667-7884"
                },
                new Paciente
                {
                    NomePaciente = "Uanderson Lima",
                    CPF = "068.296.335-67",
                    EmailPaciente = "uandersonlima@gmail.com",
                    DDD = "71",
                    Telefone = "99667-7884"
                },
                new Paciente
                {
                    NomePaciente = "Uanderson Lima",
                    CPF = "068.296.335-67",
                    EmailPaciente = "uandersonlima@gmail.com",
                    DDD = "71",
                    Telefone = "99667-7884"
                },
                new Paciente
                {
                    NomePaciente = "Uanderson Lima",
                    CPF = "068.296.335-67",
                    EmailPaciente = "uandersonlima@gmail.com",
                    DDD = "71",
                    Telefone = "99667-7884"
                }
            };
            return PartialView(listPacientes);
        }
    }
}