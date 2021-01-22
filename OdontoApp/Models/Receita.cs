using OdontoApp.Models.ClassesRelacionais;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoApp.Models
{
    public class Receita
    {
        public int ReceitaId { get; set; }
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public List<ReceitaMedicamento> ReceitaMedicamentos { get; set; }
        public List<Receituario> Receituarios { get; set; }
        public List<ReceitaMedico> ReceitaMedicos { get; set; }
    }
}
