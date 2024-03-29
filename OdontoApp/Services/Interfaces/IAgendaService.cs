﻿using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IAgendaService : IServiceBase<Agenda>
    {
        Task<PaginationList<Agenda>> GetByPatientIdAsync(AppView appQuery, int pacienteId);
    }
}
