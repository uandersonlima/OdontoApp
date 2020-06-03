using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface ITratamentoRepository:IRepositoryBase<Tratamento>
    {
        Task<PaginationList<Tratamento>> GetByPatientAsync(AppView appQuery, int pacienteId, int userId);
    }
}
