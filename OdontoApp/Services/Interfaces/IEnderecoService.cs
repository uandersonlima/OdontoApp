using OdontoApp.Models;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco> GetFullAdressByIdAsync(int entityId);
    }
}
