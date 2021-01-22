using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OdontoApp.Migrations
{
    public partial class OdontoV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessCodes",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: false),
                    CodeType = table.Column<string>(nullable: true),
                    DataGerada = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessCodes", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bairro",
                columns: table => new
                {
                    BairroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bairro", x => x.BairroId);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoCategoria = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Cep",
                columns: table => new
                {
                    CepId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cep", x => x.CepId);
                });

            migrationBuilder.CreateTable(
                name: "Cidade",
                columns: table => new
                {
                    CidadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.CidadeId);
                });

            migrationBuilder.CreateTable(
                name: "CodigosPromocionais",
                columns: table => new
                {
                    CodigoPromocionalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndentificadorPlano = table.Column<string>(nullable: true),
                    Codigo = table.Column<string>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosPromocionais", x => x.CodigoPromocionalId);
                });

            migrationBuilder.CreateTable(
                name: "DentesRegiao",
                columns: table => new
                {
                    DentesRegiaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentesRegiao", x => x.DentesRegiaoId);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    EstadoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.EstadoId);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationCode = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationCode);
                });

            migrationBuilder.CreateTable(
                name: "Pergunta",
                columns: table => new
                {
                    PerguntaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoPergunta = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pergunta", x => x.PerguntaId);
                });

            migrationBuilder.CreateTable(
                name: "Resposta",
                columns: table => new
                {
                    RespostaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao1 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resposta", x => x.RespostaId);
                });

            migrationBuilder.CreateTable(
                name: "Rua",
                columns: table => new
                {
                    RuaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rua", x => x.RuaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoPergunta",
                columns: table => new
                {
                    TipoPerguntaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoTipoPergunta = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPergunta", x => x.TipoPerguntaId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnamnesesPerguntas",
                columns: table => new
                {
                    AnamneseId = table.Column<int>(nullable: false),
                    PerguntaAnamneseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnamnesesPerguntas", x => new { x.AnamneseId, x.PerguntaAnamneseId });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    AgendaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Inicio = table.Column<DateTime>(nullable: false),
                    Fim = table.Column<DateTime>(nullable: true),
                    Situacao = table.Column<string>(nullable: true),
                    Realizado = table.Column<bool>(nullable: false),
                    PacienteId = table.Column<int>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true),
                    MedicoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.AgendaId);
                });

            migrationBuilder.CreateTable(
                name: "Anamnese",
                columns: table => new
                {
                    AnamneseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoAnamnese = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true),
                    PacienteId = table.Column<int>(nullable: true),
                    MedicoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anamnese", x => x.AnamneseId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Atestado",
                columns: table => new
                {
                    AtestadoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoAtestado = table.Column<string>(nullable: false),
                    DataAtestado = table.Column<DateTime>(nullable: false),
                    NMasAtestado = table.Column<string>(nullable: false),
                    ObsAtestado = table.Column<string>(nullable: false),
                    CidAtestado = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true),
                    PacienteId = table.Column<int>(nullable: false),
                    MedicoId = table.Column<int>(nullable: false),
                    EnderecoId = table.Column<int>(nullable: false),
                    ClinicaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atestado", x => x.AtestadoId);
                });

            migrationBuilder.CreateTable(
                name: "Caixa",
                columns: table => new
                {
                    CaixaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoCaixa = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixa", x => x.CaixaId);
                });

            migrationBuilder.CreateTable(
                name: "CargoClinica",
                columns: table => new
                {
                    CargoClinicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoCargoClinica = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoClinica", x => x.CargoClinicaId);
                });

            migrationBuilder.CreateTable(
                name: "Clinica",
                columns: table => new
                {
                    ClinicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeClinica = table.Column<string>(nullable: false),
                    CnpjClinica = table.Column<string>(nullable: false),
                    TelefoneClinica = table.Column<string>(nullable: false),
                    QuantidaDeCadeiraClinica = table.Column<string>(nullable: false),
                    EnderecoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinica", x => x.ClinicaId);
                });

            migrationBuilder.CreateTable(
                name: "ClinicaCargoClinica",
                columns: table => new
                {
                    ClinicaId = table.Column<int>(nullable: false),
                    CargoClinicaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicaCargoClinica", x => new { x.ClinicaId, x.CargoClinicaId });
                    table.ForeignKey(
                        name: "FK_ClinicaCargoClinica_CargoClinica_CargoClinicaId",
                        column: x => x.CargoClinicaId,
                        principalTable: "CargoClinica",
                        principalColumn: "CargoClinicaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClinicaCargoClinica_Clinica_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinica",
                        principalColumn: "ClinicaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    DespesaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoDespesa = table.Column<string>(nullable: false),
                    DataDespesa = table.Column<DateTime>(nullable: false),
                    ComprovanteDespesa = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true),
                    CaixaId = table.Column<int>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.DespesaId);
                    table.ForeignKey(
                        name: "FK_Despesa_Caixa_CaixaId",
                        column: x => x.CaixaId,
                        principalTable: "Caixa",
                        principalColumn: "CaixaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Despesa_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Imagem",
                columns: table => new
                {
                    ImagemId = table.Column<Guid>(nullable: false),
                    Diretorio = table.Column<string>(nullable: true),
                    PacienteId = table.Column<int>(nullable: false),
                    MedicoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagem", x => x.ImagemId);
                });

            migrationBuilder.CreateTable(
                name: "Medicamento",
                columns: table => new
                {
                    MedicamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoMedicamento = table.Column<string>(nullable: false),
                    StatusMedicamentoId = table.Column<int>(nullable: false),
                    PosologiaId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamento", x => x.MedicamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    MedicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeMedico = table.Column<string>(nullable: false),
                    NumeroCroMedico = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.MedicoId);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    EnderecoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroEndereco = table.Column<string>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    CidadeId = table.Column<int>(nullable: false),
                    BairroId = table.Column<int>(nullable: false),
                    RuaId = table.Column<int>(nullable: false),
                    CepId = table.Column<int>(nullable: false),
                    MedicoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.EnderecoId);
                    table.ForeignKey(
                        name: "FK_Endereco_Bairro_BairroId",
                        column: x => x.BairroId,
                        principalTable: "Bairro",
                        principalColumn: "BairroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Cep_CepId",
                        column: x => x.CepId,
                        principalTable: "Cep",
                        principalColumn: "CepId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "CidadeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "EstadoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medico",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Rua_RuaId",
                        column: x => x.RuaId,
                        principalTable: "Rua",
                        principalColumn: "RuaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AccessType = table.Column<string>(nullable: true),
                    PaymentStatus = table.Column<string>(nullable: true),
                    PlanNumber = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    Nascimento = table.Column<DateTime>(nullable: false),
                    Sexo = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(nullable: false),
                    EnderecoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Messagecode = table.Column<Guid>(nullable: false),
                    SenderUserId = table.Column<string>(nullable: true),
                    ReceiverUserId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TimeSent = table.Column<DateTime>(nullable: true),
                    TimeReceived = table.Column<DateTime>(nullable: true),
                    ViewedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Messagecode);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_ReceiverUserId",
                        column: x => x.ReceiverUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerguntaAnamnese",
                columns: table => new
                {
                    PerguntaAnamneseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPerguntaId = table.Column<int>(nullable: false),
                    PerguntaId = table.Column<int>(nullable: false),
                    RespostaId = table.Column<int>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true),
                    MedicoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntaAnamnese", x => x.PerguntaAnamneseId);
                    table.ForeignKey(
                        name: "FK_PerguntaAnamnese_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medico",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerguntaAnamnese_Pergunta_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Pergunta",
                        principalColumn: "PerguntaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerguntaAnamnese_Resposta_RespostaId",
                        column: x => x.RespostaId,
                        principalTable: "Resposta",
                        principalColumn: "RespostaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerguntaAnamnese_TipoPergunta_TipoPerguntaId",
                        column: x => x.TipoPerguntaId,
                        principalTable: "TipoPergunta",
                        principalColumn: "TipoPerguntaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerguntaAnamnese_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    PlanoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomePlano = table.Column<string>(nullable: true),
                    NumeroPlano = table.Column<string>(nullable: true),
                    CpfResponsavelPlano = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plano", x => x.PlanoId);
                    table.ForeignKey(
                        name: "FK_Plano_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posologia",
                columns: table => new
                {
                    PosologiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoPosologia = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posologia", x => x.PosologiaId);
                    table.ForeignKey(
                        name: "FK_Posologia_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true),
                    EstoqueMinimo = table.Column<int>(nullable: false),
                    EstoqueMaximo = table.Column<int>(nullable: false),
                    Marca = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produto_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.ReceitaId);
                    table.ForeignKey(
                        name: "FK_Receita_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatusMedicamento",
                columns: table => new
                {
                    StatusMedicamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoStatusMedicamento = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusMedicamento", x => x.StatusMedicamentoId);
                    table.ForeignKey(
                        name: "FK_StatusMedicamento_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    PacienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomePaciente = table.Column<string>(nullable: false),
                    Sexo = table.Column<string>(nullable: false),
                    Nascimento = table.Column<DateTime>(nullable: false),
                    CPF = table.Column<string>(maxLength: 14, nullable: false),
                    RgPaciente = table.Column<string>(maxLength: 14, nullable: false),
                    ObsPaciente = table.Column<string>(nullable: false),
                    EmailPaciente = table.Column<string>(nullable: false),
                    ComoChegouPaciente = table.Column<string>(nullable: false),
                    DDD = table.Column<string>(maxLength: 2, nullable: false),
                    Telefone = table.Column<string>(maxLength: 9, nullable: false),
                    NumeroProntuarioPaciente = table.Column<string>(nullable: false),
                    PlanoId = table.Column<int>(nullable: true),
                    EnderecoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.PacienteId);
                    table.ForeignKey(
                        name: "FK_Paciente_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Paciente_Plano_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "PlanoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Paciente_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Estoque",
                columns: table => new
                {
                    EstoqueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorIndividual = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(10, 2)", nullable: true),
                    ProdutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoque", x => x.EstoqueId);
                    table.ForeignKey(
                        name: "FK_Estoque_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaMedicamentos",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(nullable: false),
                    MedicamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaMedicamentos", x => new { x.MedicamentoId, x.ReceitaId });
                    table.ForeignKey(
                        name: "FK_ReceitaMedicamentos_Medicamento_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "Medicamento",
                        principalColumn: "MedicamentoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceitaMedicamentos_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaMedico",
                columns: table => new
                {
                    MedicoId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaMedico", x => new { x.ReceitaId, x.MedicoId });
                    table.ForeignKey(
                        name: "FK_ReceitaMedico_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medico",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceitaMedico_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orcamento",
                columns: table => new
                {
                    OrcamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoOrcamento = table.Column<string>(nullable: false),
                    DataOrcamento = table.Column<DateTime>(nullable: false),
                    ValorOrcamento = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    ObsOrcamento = table.Column<string>(nullable: false),
                    ValorDescontoOrcamento = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    PlanoId = table.Column<int>(nullable: true),
                    MedicoId = table.Column<int>(nullable: true),
                    PacienteId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true),
                    DentesRegiaoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamento", x => x.OrcamentoId);
                    table.ForeignKey(
                        name: "FK_Orcamento_DentesRegiao_DentesRegiaoId",
                        column: x => x.DentesRegiaoId,
                        principalTable: "DentesRegiao",
                        principalColumn: "DentesRegiaoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orcamento_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medico",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orcamento_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orcamento_Plano_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "PlanoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orcamento_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receituario",
                columns: table => new
                {
                    ReceituarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnderecoId = table.Column<int>(nullable: false),
                    PacienteId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false),
                    MedicoId = table.Column<int>(nullable: false),
                    ClinicaId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receituario", x => x.ReceituarioId);
                    table.ForeignKey(
                        name: "FK_Receituario_Clinica_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinica",
                        principalColumn: "ClinicaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receituario_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receituario_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medico",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receituario_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receituario_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receituario_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tratamento",
                columns: table => new
                {
                    TratamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTratamento = table.Column<string>(nullable: false),
                    ValorTratamento = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    DentesRegiaoId = table.Column<int>(nullable: false),
                    PlanoId = table.Column<int>(nullable: true),
                    PacienteId = table.Column<int>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamento", x => x.TratamentoId);
                    table.ForeignKey(
                        name: "FK_Tratamento_DentesRegiao_DentesRegiaoId",
                        column: x => x.DentesRegiaoId,
                        principalTable: "DentesRegiao",
                        principalColumn: "DentesRegiaoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tratamento_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tratamento_Plano_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "PlanoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tratamento_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntradaSaida",
                columns: table => new
                {
                    EntradaSaidaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    DataTransacao = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    EstoqueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradaSaida", x => x.EntradaSaidaId);
                    table.ForeignKey(
                        name: "FK_EntradaSaida_Estoque_EstoqueId",
                        column: x => x.EstoqueId,
                        principalTable: "Estoque",
                        principalColumn: "EstoqueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrcamentosTratamentos",
                columns: table => new
                {
                    TratamentoId = table.Column<int>(nullable: false),
                    OrcamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrcamentosTratamentos", x => new { x.TratamentoId, x.OrcamentoId });
                    table.ForeignKey(
                        name: "FK_OrcamentosTratamentos_Orcamento_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "Orcamento",
                        principalColumn: "OrcamentoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrcamentosTratamentos_Tratamento_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamento",
                        principalColumn: "TratamentoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PacienteTratamento",
                columns: table => new
                {
                    TratamentoId = table.Column<int>(nullable: false),
                    PacienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteTratamento", x => new { x.PacienteId, x.TratamentoId });
                    table.ForeignKey(
                        name: "FK_PacienteTratamento_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PacienteTratamento_Tratamento_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamento",
                        principalColumn: "TratamentoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recebimento",
                columns: table => new
                {
                    RecebimentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRecebimento = table.Column<DateTime>(nullable: false),
                    ComprovanteRecebimento = table.Column<string>(nullable: false),
                    PacienteId = table.Column<int>(nullable: false),
                    PlanoId = table.Column<int>(nullable: false),
                    TratamentoId = table.Column<int>(nullable: false),
                    DentesRegiaoId = table.Column<int>(nullable: false),
                    MedicoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recebimento", x => x.RecebimentoId);
                    table.ForeignKey(
                        name: "FK_Recebimento_DentesRegiao_DentesRegiaoId",
                        column: x => x.DentesRegiaoId,
                        principalTable: "DentesRegiao",
                        principalColumn: "DentesRegiaoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recebimento_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medico",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recebimento_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recebimento_Plano_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "PlanoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recebimento_Tratamento_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamento",
                        principalColumn: "TratamentoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recebimento_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_MedicoId",
                table: "Agenda",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_PacienteId",
                table: "Agenda",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_UsuarioId",
                table: "Agenda",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Anamnese_MedicoId",
                table: "Anamnese",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anamnese_PacienteId",
                table: "Anamnese",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Anamnese_UsuarioId",
                table: "Anamnese",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AnamnesesPerguntas_PerguntaAnamneseId",
                table: "AnamnesesPerguntas",
                column: "PerguntaAnamneseId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EnderecoId",
                table: "AspNetUsers",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Atestado_ClinicaId",
                table: "Atestado",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Atestado_EnderecoId",
                table: "Atestado",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atestado_MedicoId",
                table: "Atestado",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atestado_PacienteId",
                table: "Atestado",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Atestado_UsuarioId",
                table: "Atestado",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_UsuarioId",
                table: "Caixa",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoClinica_UsuarioId",
                table: "CargoClinica",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinica_EnderecoId",
                table: "Clinica",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinica_UsuarioId",
                table: "Clinica",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicaCargoClinica_CargoClinicaId",
                table: "ClinicaCargoClinica",
                column: "CargoClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_CaixaId",
                table: "Despesa",
                column: "CaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_CategoriaId",
                table: "Despesa",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_UsuarioId",
                table: "Despesa",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_BairroId",
                table: "Endereco",
                column: "BairroId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_CepId",
                table: "Endereco",
                column: "CepId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_CidadeId",
                table: "Endereco",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_EstadoId",
                table: "Endereco",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_MedicoId",
                table: "Endereco",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_RuaId",
                table: "Endereco",
                column: "RuaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaSaida_EstoqueId",
                table: "EntradaSaida",
                column: "EstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_ProdutoId",
                table: "Estoque",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagem_MedicoId",
                table: "Imagem",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagem_PacienteId",
                table: "Imagem",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagem_UsuarioId",
                table: "Imagem",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_PosologiaId",
                table: "Medicamento",
                column: "PosologiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_StatusMedicamentoId",
                table: "Medicamento",
                column: "StatusMedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_UsuarioId",
                table: "Medicamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Medico_UsuarioId",
                table: "Medico",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverUserId",
                table: "Message",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderUserId",
                table: "Message",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_DentesRegiaoId",
                table: "Orcamento",
                column: "DentesRegiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_MedicoId",
                table: "Orcamento",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_PacienteId",
                table: "Orcamento",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_PlanoId",
                table: "Orcamento",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_UsuarioId",
                table: "Orcamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentosTratamentos_OrcamentoId",
                table: "OrcamentosTratamentos",
                column: "OrcamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_EnderecoId",
                table: "Paciente",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_PlanoId",
                table: "Paciente",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_UsuarioId",
                table: "Paciente",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteTratamento_TratamentoId",
                table: "PacienteTratamento",
                column: "TratamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntaAnamnese_MedicoId",
                table: "PerguntaAnamnese",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntaAnamnese_PerguntaId",
                table: "PerguntaAnamnese",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntaAnamnese_RespostaId",
                table: "PerguntaAnamnese",
                column: "RespostaId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntaAnamnese_TipoPerguntaId",
                table: "PerguntaAnamnese",
                column: "TipoPerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntaAnamnese_UsuarioId",
                table: "PerguntaAnamnese",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Plano_UsuarioId",
                table: "Plano",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Posologia_UsuarioId",
                table: "Posologia",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_UsuarioId",
                table: "Produto",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_DentesRegiaoId",
                table: "Recebimento",
                column: "DentesRegiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_MedicoId",
                table: "Recebimento",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_PacienteId",
                table: "Recebimento",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_PlanoId",
                table: "Recebimento",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_TratamentoId",
                table: "Recebimento",
                column: "TratamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_UsuarioId",
                table: "Recebimento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_UsuarioId",
                table: "Receita",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaMedicamentos_ReceitaId",
                table: "ReceitaMedicamentos",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaMedico_MedicoId",
                table: "ReceitaMedico",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Receituario_ClinicaId",
                table: "Receituario",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Receituario_EnderecoId",
                table: "Receituario",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Receituario_MedicoId",
                table: "Receituario",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Receituario_PacienteId",
                table: "Receituario",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Receituario_ReceitaId",
                table: "Receituario",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Receituario_UsuarioId",
                table: "Receituario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusMedicamento_UsuarioId",
                table: "StatusMedicamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_DentesRegiaoId",
                table: "Tratamento",
                column: "DentesRegiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_PacienteId",
                table: "Tratamento",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_PlanoId",
                table: "Tratamento",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_UsuarioId",
                table: "Tratamento",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnamnesesPerguntas_Anamnese_AnamneseId",
                table: "AnamnesesPerguntas",
                column: "AnamneseId",
                principalTable: "Anamnese",
                principalColumn: "AnamneseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnamnesesPerguntas_PerguntaAnamnese_PerguntaAnamneseId",
                table: "AnamnesesPerguntas",
                column: "PerguntaAnamneseId",
                principalTable: "PerguntaAnamnese",
                principalColumn: "PerguntaAnamneseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Medico_MedicoId",
                table: "Agenda",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "MedicoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Paciente_PacienteId",
                table: "Agenda",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "PacienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_AspNetUsers_UsuarioId",
                table: "Agenda",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Anamnese_Medico_MedicoId",
                table: "Anamnese",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "MedicoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Anamnese_Paciente_PacienteId",
                table: "Anamnese",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "PacienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Anamnese_AspNetUsers_UsuarioId",
                table: "Anamnese",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Atestado_Medico_MedicoId",
                table: "Atestado",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "MedicoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Atestado_Paciente_PacienteId",
                table: "Atestado",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "PacienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Atestado_AspNetUsers_UsuarioId",
                table: "Atestado",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Atestado_Endereco_EnderecoId",
                table: "Atestado",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Atestado_Clinica_ClinicaId",
                table: "Atestado",
                column: "ClinicaId",
                principalTable: "Clinica",
                principalColumn: "ClinicaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Caixa_AspNetUsers_UsuarioId",
                table: "Caixa",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CargoClinica_AspNetUsers_UsuarioId",
                table: "CargoClinica",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clinica_AspNetUsers_UsuarioId",
                table: "Clinica",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clinica_Endereco_EnderecoId",
                table: "Clinica",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesa_AspNetUsers_UsuarioId",
                table: "Despesa",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Imagem_Medico_MedicoId",
                table: "Imagem",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "MedicoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Imagem_Paciente_PacienteId",
                table: "Imagem",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "PacienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Imagem_AspNetUsers_UsuarioId",
                table: "Imagem",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamento_AspNetUsers_UsuarioId",
                table: "Medicamento",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamento_Posologia_PosologiaId",
                table: "Medicamento",
                column: "PosologiaId",
                principalTable: "Posologia",
                principalColumn: "PosologiaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamento_StatusMedicamento_StatusMedicamentoId",
                table: "Medicamento",
                column: "StatusMedicamentoId",
                principalTable: "StatusMedicamento",
                principalColumn: "StatusMedicamentoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_AspNetUsers_UsuarioId",
                table: "Medico",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Medico_MedicoId",
                table: "Endereco");

            migrationBuilder.DropTable(
                name: "AccessCodes");

            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropTable(
                name: "AnamnesesPerguntas");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Atestado");

            migrationBuilder.DropTable(
                name: "ClinicaCargoClinica");

            migrationBuilder.DropTable(
                name: "CodigosPromocionais");

            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "EntradaSaida");

            migrationBuilder.DropTable(
                name: "Imagem");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "OrcamentosTratamentos");

            migrationBuilder.DropTable(
                name: "PacienteTratamento");

            migrationBuilder.DropTable(
                name: "Recebimento");

            migrationBuilder.DropTable(
                name: "ReceitaMedicamentos");

            migrationBuilder.DropTable(
                name: "ReceitaMedico");

            migrationBuilder.DropTable(
                name: "Receituario");

            migrationBuilder.DropTable(
                name: "Anamnese");

            migrationBuilder.DropTable(
                name: "PerguntaAnamnese");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CargoClinica");

            migrationBuilder.DropTable(
                name: "Caixa");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Estoque");

            migrationBuilder.DropTable(
                name: "Orcamento");

            migrationBuilder.DropTable(
                name: "Tratamento");

            migrationBuilder.DropTable(
                name: "Medicamento");

            migrationBuilder.DropTable(
                name: "Clinica");

            migrationBuilder.DropTable(
                name: "Receita");

            migrationBuilder.DropTable(
                name: "Pergunta");

            migrationBuilder.DropTable(
                name: "Resposta");

            migrationBuilder.DropTable(
                name: "TipoPergunta");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "DentesRegiao");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Posologia");

            migrationBuilder.DropTable(
                name: "StatusMedicamento");

            migrationBuilder.DropTable(
                name: "Plano");

            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Bairro");

            migrationBuilder.DropTable(
                name: "Cep");

            migrationBuilder.DropTable(
                name: "Cidade");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Rua");
        }
    }
}
