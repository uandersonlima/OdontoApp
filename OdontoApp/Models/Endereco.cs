using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdontoApp.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        [Display (Name ="Número")]
        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        public string NumeroEndereco { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }
        public int BairroId { get; set; }
        public Bairro Bairro { get; set; }
        public int RuaId { get; set; }
        public Rua Rua { get; set; }
        public int CepId { get; set; }
        public Cep Cep { get; set; }
        public Medico Medico { get; set; }
      
        public List<Paciente> Pacientes { get; set; }
        public List<Atestado> Atestados { get; set; }
        public List<Receituario> Receituarios { get; set; }
        public List<Clinica> Clinicas { get; set; }

    }
}
