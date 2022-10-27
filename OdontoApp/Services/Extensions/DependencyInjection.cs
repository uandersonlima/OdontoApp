using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Interfaces;
using System;

namespace OdontoApp.Services.Extensions
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection svc, IConfiguration conf)
        {
            svc.Configure<ApiBehaviorOptions>(op =>
            {
                op.SuppressModelStateInvalidFilter = true;
            });

            svc.AddControllersWithViews().AddNewtonsoftJson(opt => { opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });

            svc.AddHttpContextAccessor();

            svc.AddSignalR();

            svc.Configure<AppSettings>(conf.GetSection(AppSettings.Position));

            svc.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1000);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            svc.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1000);
                options.LoginPath = "/Auth/SignIn";
                options.AccessDeniedPath = "/Auth/AccessDenied";
                options.SlidingExpiration = true;
            });

            svc.AddAuthentication()
            .AddGoogle(googleOptions =>
            {
                IConfigurationSection googleAuthNSection = conf.GetSection("Auth:GoogleOAuth2");
                googleOptions.ClientId = googleAuthNSection["ClientId"];
                googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            //Banco de Dados
            svc.AddDbContext<OdontoAppContext>(options =>
            options.UseMySql(conf.GetConnectionString("testeContext"),
                                     x => x.MigrationsAssembly("OdontoApp")));

            svc.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<OdontoAppContext>().AddDefaultTokenProviders();

            //Injeções Repositorios
            svc.AddScoped<IAgendaRepository, AgendaRepository>();
            svc.AddScoped<IAnamneseRepository, AnamneseRepository>();
            svc.AddScoped<IClinicaRepository, ClinicaRepository>();
            svc.AddScoped<IKeyRepository, KeyRepository>();
            svc.AddScoped<ICodigoPromocionalRepository, CodigoPromocionalRepository>();
            svc.AddScoped<IDentesRegiaoRepository, DentesRegiaoRepository>();
            svc.AddScoped<IEnderecoRepository, EnderecoRepository>();
            svc.AddScoped<IEntradaSaidaRepository, EntradaSaidaRepository>();
            svc.AddScoped<IEstoqueRepository, EstoqueRepository>();
            svc.AddScoped<IMedicoRepository, MedicoRepository>();
            svc.AddScoped<IMessageRepository, MessageRepository>();
            svc.AddScoped<IOrcamentoRepository, OrcamentoRepository>();
            svc.AddScoped<IPacienteRepository, PacienteRepository>();
            svc.AddScoped<IPerguntaAnamneseRepository, PerguntaAnamneseRepository>();
            svc.AddScoped<IPlanoRepository, PlanoRepository>();
            svc.AddScoped<IProdutoRepository, ProdutoRepository>();
            svc.AddScoped<ITipoPerguntaRepository, TipoPerguntaRepository>();
            svc.AddScoped<ITratamentoRepository, TratamentoRepository>();
            svc.AddScoped<IUsuarioRepository, UsuarioRepository>();



            //Injeções Serviços
            svc.AddScoped<IAgendaService, AgendaService>();
            svc.AddScoped<IAnamneseService, AnamneseService>();
            svc.AddScoped<IAuthService, AuthService>();
            svc.AddScoped<IClinicaService, ClinicaService>();
            svc.AddScoped<IKeyService, KeyService>();
            svc.AddScoped<ICodigoPromocionalService, CodigoPromocionalService>();
            svc.AddScoped<IDentesRegiaoService, DentesRegiaoService>();
            svc.AddScoped<IEmailService, EmailService>();
            svc.AddScoped<IEnderecoService, EnderecoService>();
            svc.AddScoped<IEntradaSaidaService, EntradaSaidaService>();
            svc.AddScoped<IEstoqueService, EstoqueService>();
            svc.AddScoped<IMedicoService, MedicoService>();
            svc.AddScoped<IMessageService, MessageService>();
            svc.AddScoped<IOrcamentoService, OrcamentoService>();
            svc.AddScoped<IPacienteService, PacienteService>();
            svc.AddScoped<IPerguntaAnamneseService, PerguntaAnamneseService>();
            svc.AddScoped<IPlanoService, PlanoService>();
            svc.AddScoped<IProdutoService, ProdutoService>();
            svc.AddScoped<ITipoPerguntaService, TipoPerguntaService>();
            svc.AddScoped<ITratamentoService, TratamentoService>();
            svc.AddScoped<IUsuarioService, UsuarioService>();

            //Startup of app
            svc.AddScoped<InicializarDados>();

            svc.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyOrigin();
                    });
            });
        }
    }
}
