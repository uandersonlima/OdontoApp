
using OdontoApp.Models.Estoque;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IEntradaSaidaService : IServiceBase<EntradaSaida>
    {
        Task<PaginationList<EntradaSaida>> GetAllInputsAsync(AppView appQuery);
        Task<PaginationList<EntradaSaida>> GetAllOutputsAsync(AppView appQuery);
        Task AddProductAsync(EntradaSaida entity);
        Task SubstractProductAsync(EntradaSaida entity);
    }
}
