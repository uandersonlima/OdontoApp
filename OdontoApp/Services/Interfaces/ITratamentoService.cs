using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface ITratamentoService : IServiceBase<Tratamento>
    {
        Task<PaginationList<Tratamento>> GetByPatientAsync(AppView appQuery, int pacienteId);
    }
}
