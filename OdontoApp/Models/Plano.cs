﻿using OdontoApp.Libraries.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Plano
    {
        public int PlanoId { get; set; }

        [Display(Name = "Nome do Plano")]
        [DataType(DataType.Text)]
        public string NomePlano { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        [Display(Name = "Número do Plano")]
        public string NumeroPlano { get; set; }

        [CPF(ErrorMessage = "Informe um {0} válido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0}")]
        [Display(Name ="CPF")]
        public string CpfResponsavelPlano { get; set; }


        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

        public List<Paciente> Pacientes { get; set; } 
        public List<Tratamento> Tratamentos { get; set; } 
        public List<Orcamento> Orcamentos { get; set; }
        public List<Recebimento> Recebimentos { get; set; }
    }
}
