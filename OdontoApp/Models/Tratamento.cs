using OdontoApp.Models.ClassesRelacionais;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Tratamento
    {
        public int TratamentoId { get; set; }


        [Display(Name = "Nome do Tratamento")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Informe o Campo {0}", AllowEmptyStrings = false)]
        public string NomeTratamento { get; set; }

        [Display(Name = "Valor do Tratamento")]
        [Required(ErrorMessage = "Informe o Campo {0}", AllowEmptyStrings = false)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorTratamento { get; set; }

        public int DentesRegiaoId { get; set; }
        public DentesRegiao DentesRegiao { get; set; }

        public int? PlanoId { get; set; }
        public Plano Plano { get; set; }

        public int? PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public List<OrcamentoTratamento> TratamentoOrcamentos { get; set; }
        public List<Recebimento> Recebimentos { get; set; }
    }
}
