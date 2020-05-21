using OdontoApp.Models.ClassesRelacionais;
using System.Collections.Generic;


namespace OdontoApp.Models
{
    public class Receita
    {
        public int ReceitaId { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<ReceitaMedicamento> ReceitaMedicamentos { get; set; }
        public List<Receituario> Receituarios { get; set; }
        public List<ReceitaMedico> ReceitaMedicos { get; set; }
    }
}
