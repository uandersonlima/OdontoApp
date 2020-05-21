using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace OdontoApp.Models.ViewModel
{
    [NotMapped]
    public class PacienteFormViewModel
    {
        public Paciente Paciente { get; set; }
        public List<Endereco>Enderecos { get; set; }
        public List<Estado> Estados { get; set; }
        public List<Cidade> Cidades { get; set; }
        public List<Bairro> Bairros { get; set; }
        public List<Rua> Ruas { get; set; }
        public List<Cep> Ceps { get; set; }
        public List<Plano> Planos { get; set; }
    
    }
}
