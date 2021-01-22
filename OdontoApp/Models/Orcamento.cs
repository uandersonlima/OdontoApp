using OdontoApp.Models.ClassesRelacionais;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Orcamento
    {
        public int OrcamentoId { get; set; }

        [Display(Name = "Descrição do orçamento")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Informe o Campo {0}", AllowEmptyStrings = false)]
        public string DescricaoOrcamento { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Data do Orçamento")]
        [Required(ErrorMessage = "Informe o Campo {0}", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataOrcamento { get; set; }

        [Display(Name = "Valor do Orçamento")]
        [Required(ErrorMessage = "Informe o Campo {0}", AllowEmptyStrings = false)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorOrcamento { get; set; }

        [Display(Name = "Observações")]
        [Required(ErrorMessage = "Informe o Campo {0}", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public string ObsOrcamento { get; set; }

        [Display(Name = "Valor de Desconto")]
        [Required(ErrorMessage = "Informe o Campo {0}", AllowEmptyStrings = false)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorDescontoOrcamento { get; set; }


        public int? PlanoId { get; set; }
        public Plano Plano { get; set; }

        public int? MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }


        public List<OrcamentoTratamento> OrcamentoTratamentos { get; set; }
    }
}
