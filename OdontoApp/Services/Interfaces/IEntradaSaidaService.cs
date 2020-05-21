
using OdontoApp.Models.Estoque;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IEntradaSaidaService : IServiceBase<EntradaSaida>
    {
        Task<PaginationList<EntradaSaida>> GetAllInputsAsync(AppQuery appQuery);
        Task<PaginationList<EntradaSaida>> GetAllOutputsAsync(AppQuery appQuery);
        Task AddProductAsync(EntradaSaida entity);
        Task SubstractProductAsync(EntradaSaida entity);
    }
}
