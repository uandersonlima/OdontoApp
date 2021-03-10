using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IAnamneseService : IServiceBase<Anamnese>
    {
        Task AddAsync(Anamnese entity, List<int> listPerguntaId);
        Task AddAnamneseToPacienteAsync(Anamnese entity, int pacienteId);
        Task ExcludePacienteAnamneseAsync(int pacienteId, int anamneseId);
        Task UpdatePacienteAnamneseAsync(Anamnese entity);
        Task UpdateAsync(Anamnese entity, List<int> listPerguntaId);    
        Task<PaginationList<Anamnese>> GetByPatientIdAsync(int pacienteId, AppView appview);
        Task<List<int>> CheckBoxChecked(int anamneseId);
    }
}
