using OdontoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IAnamneseService : IServiceBase<Anamnese>
    {
        Task AddAsync(Anamnese entity, List<int> listPerguntaId);
        Task AddPacienteToAnamneseAsync(Anamnese entity, int pacienteId);
        Task ExcludePacienteAnamnese(int pacienteId, int anamneseId);
        Task UpdatePacienteAnamneseAsync(Anamnese entity);
        Task UpdateAsync(Anamnese entity, List<int> listPerguntaId);       
        Task<List<int>> CheckBoxChecked(int anamneseId);
    }
}
