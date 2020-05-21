using OdontoApp.Models.Estoque;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IEstoqueService : IServiceBase<Estoque>
    {
        Task<Estoque> LastStockAddedAsync();
    }
}
