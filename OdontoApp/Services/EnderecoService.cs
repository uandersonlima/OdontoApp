using OdontoApp.Models;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class EnderecoService:IEnderecoService
    {
        private readonly IEnderecoRepository enderecoRepos;

        public EnderecoService(IEnderecoRepository enderecoRepos)
        {
            this.enderecoRepos = enderecoRepos;
        }

        public async Task<Endereco> GetFullAdressByIdAsync(int entityId)
        {
            return await enderecoRepos.GetFullAdressByIdAsync(entityId);
        }
    }
}
