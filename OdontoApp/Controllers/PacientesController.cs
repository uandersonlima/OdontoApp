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
        public IActionResult ListaPacientes(AppView appquery)
        {
            var list = new PaginationList<Paciente>
            {
                Pagination = new Pagination { TotalPages = 1, NumberPag = 1, TotalRecords = 1, RecordPerPage = 1 }
            };
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
            list.AddRange(listPacientes);
            return PartialView(list );
        }
    }
}