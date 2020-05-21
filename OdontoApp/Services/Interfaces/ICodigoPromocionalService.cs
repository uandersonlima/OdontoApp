using OdontoApp.Models.Promocoes;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface ICodigoPromocionalService:IServiceBase<CodigoPromocional>
    {
        Task<CodigoPromocional> GetByPlanAsync(string planCode);
        Task<string> VerificarCodigoPromocional(string planCode);
    }
}
