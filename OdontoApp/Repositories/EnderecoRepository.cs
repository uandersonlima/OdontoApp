using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly OdontoAppContext context;

        public EnderecoRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task<Endereco> GetFullAdressByIdAsync(int entityId)
        {
            return await context.Endereco.AsNoTracking()
                                         .Include(x => x.Bairro)
                                         .Include(x => x.Cidade)
                                         .Include(x => x.Estado)
                                         .Include(x => x.Rua)
                                         .Include(x => x.Cep)
                                         .Where(op => op.EnderecoId == entityId)
                                         .FirstOrDefaultAsync();
        }
    }
}
