using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IAnamneseRepository:IRepositoryBase<Anamnese>
    {
        Task DeleteAllAsync(Anamnese entity);
        Task<PaginationList<Anamnese>> GetByPatientIdAsync(AppView appview, int pacienteId, string UserId);
    }
}
