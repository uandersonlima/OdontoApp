using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Recebimento
    {
        public int RecebimentoId { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Recebimento")]
        public DateTime DataRecebimento { get; set; }


        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [Display(Name = "Comprovante de Recebimento")]
        public string ComprovanteRecebimento { get; set; }


        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int PlanoId { get; set; }
        public Plano Plano { get; set; }

        public int TratamentoId { get; set; }
        public Tratamento Tratamento { get; set; }

        public int DentesRegiaoId { get; set; }
        public DentesRegiao DentesRegiao { get; set; }

        public int MedicoId { get; set; }
        public Medico Medico { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}
