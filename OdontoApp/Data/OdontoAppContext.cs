using Microsoft.EntityFrameworkCore;
using OdontoApp.Models;
using OdontoApp.Models.ClassesRelacionais;
using OdontoApp.Models.AccessCode;
using OdontoApp.Models.Estoque;
using OdontoApp.Models.Promocoes;
using System.Linq;

namespace OdontoApp.Data
{
    public class OdontoAppContext : DbContext
    {

        public OdontoAppContext(DbContextOptions<OdontoAppContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<CodigoPromocional> CodigosPromocionais { get; set; }
        public DbSet<AccessCode> AccessCodes { get; set; }
        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Bairro> Bairro { get; set; }
        public DbSet<Rua> Rua { get; set; }
        public DbSet<Cep> Cep { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<EntradaSaida> EntradaSaida { get; set; }
        public DbSet<Clinica> Clinica { get; set; }
        public DbSet<CargoClinica> CargoClinica { get; set; }
        public DbSet<ClinicaCargoClinica> ClinicaCargoClinica { get; set; }
        public DbSet<TipoPergunta> TipoPergunta { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<Anamnese> Anamnese { get; set; }
        public DbSet<Resposta> Resposta { get; set; }
        public DbSet<PerguntaAnamnese> PerguntaAnamnese { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<DentesRegiao> DentesRegiao { get; set; }
        public DbSet<Tratamento> Tratamento { get; set; }
        public DbSet<PacienteTratamento> PacienteTratamento { get; set; }
        public DbSet<Atestado> Atestado { get; set; }
        public DbSet<Caixa> Caixa { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Despesa> Despesa { get; set; }
        public DbSet<Imagem> Imagem { get; set; }
        public DbSet<StatusMedicamento> StatusMedicamento { get; set; }
        public DbSet<Posologia> Posologia { get; set; }
        public DbSet<Medicamento> Medicamento { get; set; }
        public DbSet<Receita> Receita { get; set; }
        public DbSet<ReceitaMedicamento> ReceitaMedicamentos { get; set; }
        public DbSet<Receituario> Receituario { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<Recebimento> Recebimento { get; set; }
        public DbSet<ReceitaMedico> ReceitaMedico { get; set; }
        public DbSet<Plano> Plano { get; set; }
        public DbSet<AnamnesesPerguntas> AnamnesesPerguntas { get; set; }
        public DbSet<OrcamentoTratamento> OrcamentosTratamentos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
            modelBuilder.Entity<ClinicaCargoClinica>().HasKey(pk =>
                                new { pk.ClinicaId, pk.CargoClinicaId });
            modelBuilder.Entity<PacienteTratamento>().HasKey(pk => new { pk.PacienteId, pk.TratamentoId });

            modelBuilder.Entity<ReceitaMedicamento>().HasKey(pk => new { pk.MedicamentoId, pk.ReceitaId });

            modelBuilder.Entity<ReceitaMedico>().HasKey(pk => new { pk.ReceitaId, pk.MedicoId });

            modelBuilder.Entity<AnamnesesPerguntas>().HasKey(pk => new { pk.AnamneseId, pk.PerguntaAnamneseId });
            modelBuilder.Entity<OrcamentoTratamento>().HasKey(pk => new { pk.TratamentoId, pk.OrcamentoId });
        }

    }
}
