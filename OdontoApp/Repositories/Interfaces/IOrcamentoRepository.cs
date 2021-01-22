using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IOrcamentoRepository : IRepositoryBase<Orcamento>
    {
        Task<PaginationList<Orcamento>> GetByPatientAsync(AppView appQuery, int pacienteId, string userId);
    }
}
