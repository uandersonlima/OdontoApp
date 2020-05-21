using OdontoApp.Models.Estoque;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IEstoqueRepository : IRepositoryBase<Estoque>
    {
        Task<Estoque> LastStockAddedAsync();
    }
}
