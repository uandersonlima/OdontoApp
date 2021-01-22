using OdontoApp.Models.Estoque;
using OdontoApp.Models.Helpers;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IEntradaSaidaRepository:IRepositoryBase<EntradaSaida>
    {
        Task<PaginationList<EntradaSaida>> GetAllInputsAsync(AppView appQuery, string idUser);
        Task<PaginationList<EntradaSaida>> GetAllOutputsAsync(AppView appQuery, string idUser);
    }
}
