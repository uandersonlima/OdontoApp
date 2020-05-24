using Microsoft.Extensions.Configuration;
using OdontoApp.Models;
using OdontoApp.Services.Interfaces;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient smtp;
        private readonly IConfiguration conf;

        public EmailService(SmtpClient smtp, IConfiguration conf)
        {
            this.smtp = smtp;
            this.conf = conf;
        }
        public async Task SendEmailRecoveryAsync(Usuario usuario, string code_encrypted)
        {
            string corpoMsg = string.Format("<h1>DEV4 - OrtoApp</h1>" +
                               "Seu código de Recuperação é:" + $"<h2>{code_encrypted}</h2>");

            MailMessage mensagem = new MailMessage
            {
                From = new MailAddress(conf.GetValue<string>("Email:Username")),
                Subject = "DEV4 - OrtoApp - Codigo de Recuperação - " + usuario.Nome,
                Body = corpoMsg,
                IsBodyHtml = true
            };
            mensagem.To.Add(usuario.Email);
            await smtp.SendMailAsync(mensagem);
        }
        public async Task SendEmailVerificationAsync(Usuario usuario, string code_encrypted)
        {
            string corpoMsg = string.Format("<h1>DEV4 - OrtoApp</h1>" +
                                            "Seu código de Ativação é: " + $"<h2>{code_encrypted}</h2>");

            MailMessage mensagem = new MailMessage
            {
                From = new MailAddress(conf.GetValue<string>("Email:Username")),
                Subject = "DEV4 - OrtoApp - Código de Ativação - " + usuario.Nome,
                Body = corpoMsg,
                IsBodyHtml = true
            };
            mensagem.To.Add(usuario.Email);
            await smtp.SendMailAsync(mensagem);
        }

    }
}
