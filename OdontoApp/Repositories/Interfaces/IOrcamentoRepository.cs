using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IOrcamentoRepository : IRepositoryBase<Orcamento>
    {
        Task<PaginationList<Orcamento>> GetByPatientAsync(AppQuery appQuery, int pacienteId, int userId);
    }
}
