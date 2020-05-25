using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IAgendaRepository : IRepositoryBase<Agenda>
    {
        Task<PaginationList<Agenda>> GetByPatientAsync(AppView appQuery, int pacienteId, int userId);
    }
}
