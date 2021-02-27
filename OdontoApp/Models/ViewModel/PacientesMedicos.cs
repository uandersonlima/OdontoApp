using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OdontoApp.Models.ViewModel
{
    public class PacientesMedicos
    {
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public SelectList Medicos { get; set; }
        public SelectList Pacientes { get; set; }
    }
}