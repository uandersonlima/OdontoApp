﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Despesa
    {
        public int DespesaId { get; set; }
        [Required(ErrorMessage="Informe o Campo", AllowEmptyStrings = false)]
        [Display (Name = "Despesas")]
        public string DescricaoDespesa { get; set; }


        [Display(Name = "Data Despesas")]
        [Required(ErrorMessage = "Informe o Campo", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataDespesa { get; set; }

        [Display(Name ="Comprovante")]
        [Required(ErrorMessage = "Informe o Campo", AllowEmptyStrings = false)]
        public string ComprovanteDespesa { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public int CaixaId { get; set; }
        public Caixa Caixa { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
