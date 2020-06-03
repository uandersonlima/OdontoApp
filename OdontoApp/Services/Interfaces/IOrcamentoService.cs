using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IOrcamentoService : IServiceBase<Orcamento>
    {
        Task AddAsync(Orcamento entity, List<int> listTratamentoId);
        Task<List<int>> CheckBoxChecked(int id);
        Task<PaginationList<Orcamento>> GetByPatientAsync(AppView appQuery, int pacienteId);
        Task UpdateAsync(Orcamento entity, List<int> listPerguntaId);

    }
}
