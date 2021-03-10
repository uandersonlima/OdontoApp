using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using OdontoApp.Libraries.Templates;
using OdontoApp.Libraries.Texto;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings appsettings;
        private readonly IHttpContextAccessor acessor;

        public EmailService(IOptions<AppSettings> appsettings, IHttpContextAccessor acessor)
        {
            this.appsettings = appsettings.Value;
            this.acessor = acessor;
        }

        public async Task SendEmailRecoveryAsync(ApplicationUser usuario, string code_encrypted)
        {
            string nome = Mask.PrimeiroNome(usuario.Nome);    
            string recoveryLink =  $"https://{acessor.HttpContext.Request.Host}/token/{code_encrypted}";    
            string head = HtmlEmailTemplate.EmailHead();
            string body = HtmlEmailTemplate.EmailRecoveryBody(nome, recoveryLink, "https://google.com.br", "https://google.com.br");
            string Html = HtmlEmailTemplate.EmailHtml(head, body);         
            
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(appsettings.SmtpUser));
            email.To.Add(MailboxAddress.Parse(usuario.Email));
            email.Subject = "Ascore - OdontoControl - Email de Recuperação - " + usuario.Nome;
            email.Body = new TextPart(TextFormat.Html) { Text = Html };

            // Send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(appsettings.SmtpHost, appsettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(appsettings.SmtpUser, appsettings.SmtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        public async Task SendEmailVerificationAsync(ApplicationUser usuario, string code_encrypted)
        {
            string nome = Mask.PrimeiroNome(usuario.Nome);    
            string confirmationLink =  $"https://{acessor.HttpContext.Request.Host}/token/{code_encrypted}";    
            string head = HtmlEmailTemplate.EmailHead();
            string body = HtmlEmailTemplate.EmailVerificationBody(nome, confirmationLink, "https://google.com.br", "https://google.com.br");
            string Html = HtmlEmailTemplate.EmailHtml(head, body);
            
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(appsettings.SmtpUser));
            email.To.Add(MailboxAddress.Parse(usuario.Email));
            email.Subject = "Ascore - OdontoControl - Email de verificação - " + nome;
            email.Body = new TextPart(TextFormat.Html) { Text = Html };

            // Send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(appsettings.SmtpHost, appsettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(appsettings.SmtpUser, appsettings.SmtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}
