using OdontoApp.Models;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IAnamneseRepository:IRepositoryBase<Anamnese>
    {
        Task DeleteAllAsync(Anamnese entity);
    }
}
