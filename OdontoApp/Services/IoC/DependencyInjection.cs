using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdontoApp.Data;
using OdontoApp.Repositories;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Interfaces;
using System;
using System.Net;
using System.Net.Mail;

namespace OdontoApp.Services.IoC
{
    public static class DependencyInjection
    {
        public static void Injetar(IServiceCollection svc, IConfiguration conf)
        {
            svc.AddControllersWithViews().AddNewtonsoftJson(opt => { opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; }); 
            svc.AddRazorPages();
            svc.AddHttpContextAccessor();
            svc.AddScoped<IUsuarioRepository, UsuarioRepository>();
            svc.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Email - Gerenciamento
            svc.AddScoped<SmtpClient>(options =>
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = conf.GetValue<string>("Email:ServerSMTP"),
                    Port = conf.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(conf.GetValue<string>("Email:Username"), conf.GetValue<string>("Email:Password")),
                    EnableSsl = true
                };

                return smtp;
            });
            svc.AddScoped<EmailService>();
            svc.AddScoped<ICodigoRepository, CodigoRepository>();


            //Session - Configuration
            svc.AddMemoryCache(); //Guardar os dados na memória
            svc.AddSession(options =>
            {
                //// Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                //// Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            //Banco de Dados
            svc.AddDbContext<OdontoAppContext>(options =>
            options.UseSqlServer(conf.GetConnectionString("OdontoAppContext"),
                                     x => x.MigrationsAssembly("OdontoApp")));

            //Injeções Repositorios
            svc.AddScoped<IAgendaRepository, AgendaRepository>();
            svc.AddScoped<IAnamneseRepository, AnamneseRepository>();
            svc.AddScoped<IClinicaRepository, ClinicaRepository>();
            svc.AddScoped<ICodigoRepository, CodigoRepository>();
            svc.AddScoped<ICodigoPromocionalRepository, CodigoPromocionalRepository>();
            svc.AddScoped<IDentesRegiaoRepository, DentesRegiaoRepository>();
            svc.AddScoped<IEnderecoRepository, EnderecoRepository>();
            svc.AddScoped<IEntradaSaidaRepository, EntradaSaidaRepository>();
            svc.AddScoped<IEstoqueRepository, EstoqueRepository>();
            svc.AddScoped<IMedicoRepository, MedicoRepository>();
            svc.AddScoped<IOrcamentoRepository, OrcamentoRepository>();
            svc.AddScoped<IPacienteRepository, PacienteRepository>();
            svc.AddScoped<IPerguntaAnamneseRepository, PerguntaAnamneseRepository>();
            svc.AddScoped<IPlanoRepository, PlanoRepository>();
            svc.AddScoped<IProdutoRepository, ProdutoRepository>();
            svc.AddScoped<ITipoPerguntaRepository, TipoPerguntaRepository>();
            svc.AddScoped<ITratamentoRepository, TratamentoRepository>();
            svc.AddScoped<IUsuarioRepository, UsuarioRepository>();


            //Injeções do login
            svc.AddScoped<ISessionService, SessionService>();
            svc.AddScoped<ILoginService, LoginService>();

            //Injeções Serviços
            svc.AddScoped<IAgendaService, AgendaService>();
            svc.AddScoped<IAnamneseService, AnamneseService>();
            svc.AddScoped<IClinicaService, ClinicaService>();
            svc.AddScoped<ICodigoService, CodigoService>();
            svc.AddScoped<ICodigoPromocionalService, CodigoPromocionalService>();
            svc.AddScoped<IDentesRegiaoService, DentesRegiaoService>();
            svc.AddScoped<IEmailService, EmailService>();
            svc.AddScoped<IEnderecoService, EnderecoService>();
            svc.AddScoped<IEntradaSaidaService, EntradaSaidaService>();
            svc.AddScoped<IEstoqueService, EstoqueService>();
            svc.AddScoped<ILoginService, LoginService>();
            svc.AddScoped<IMedicoService, MedicoService>();
            svc.AddScoped<IOrcamentoService, OrcamentoService>();
            svc.AddScoped<IPacienteService, PacienteService>();
            svc.AddScoped<IPerguntaAnamneseService, PerguntaAnamneseService>();
            svc.AddScoped<IPlanoService, PlanoService>();
            svc.AddScoped<IProdutoService, ProdutoService>();
            svc.AddScoped<ISessionService, SessionService>();
            svc.AddScoped<ITipoPerguntaService, TipoPerguntaService>();
            svc.AddScoped<ITratamentoService, TratamentoService>();
            svc.AddScoped<IUsuarioService, UsuarioService>();

            //Startup of app
            svc.AddScoped<InicializarDados>();
        }
    }
}
