using OdontoApp.Models;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<Endereco> GetFullAdressByIdAsync(int entityId);
    }
}
