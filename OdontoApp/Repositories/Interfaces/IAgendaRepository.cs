using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IAgendaRepository : IRepositoryBase<Agenda>
    {
        Task<PaginationList<Agenda>> GetByPatientIdAsync(AppView appQuery, int pacienteId, string userId);
    }
}
