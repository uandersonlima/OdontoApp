using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Services;
using OdontoApp.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IAuthService authService;
        private readonly IUsuarioService userSvc;
        private readonly IMessageService msgSvc;
        private readonly IHubContext<MessageHubService, IMessageHubService> messageHub;

        public MessagesController(IAuthService authService, IUsuarioService userSvc, IMessageService msgSvc, IHubContext<MessageHubService, IMessageHubService> messageHub)
        {
            this.authService = authService;
            this.userSvc = userSvc;
            this.msgSvc = msgSvc;
            this.messageHub = messageHub;
        }


        [HttpGet("/messages"), UserAuthorization]
        public IActionResult Index()
        {
            return View("~/Views/Messages/Index.cshtml");
        }

        [HttpGet(""), UserAuthorization]
        public async Task<IActionResult> GetAll([FromBody] AppView appView)
        {
            var msgs = await msgSvc.GetMessagesAsync(appView);
            return Ok(msgs);
        }

        [HttpGet("{messagecode}"), UserAuthorization]
        public async Task<IActionResult> Get(string messagecode)
        {
            var msg = await msgSvc.GetMessageAsync(messagecode);
            return Ok(msg);
        }

        [HttpPut(""), UserAuthorization]
        public async Task<IActionResult> Update([FromBody] Message clientMsg)
        {
            var serverMsg = await msgSvc.GetMessageAsync(clientMsg.Messagecode);
            
            if (serverMsg == null)
                return UnprocessableEntity($"Identificador inválido {clientMsg.Messagecode} ou não existe");

            await msgSvc.UpdateMessageAsync(clientMsg);

            return Ok(clientMsg);
        }

        [HttpPost(""), UserAuthorization]
        public async Task<IActionResult> SendMessage([FromBody] Message msg)
        {
            msg.TimeSent = DateTime.Now;
            msg.SenderUserId = authService.GetLoggedUserAsync().Result.Id;
            await msgSvc.SendMessageAsync(msg);
            await messageHub.Clients.User(msg.ReceiverUserId).ReportNewMessagesAsync("nova mensagem", msg);
            return Ok(msg);
        }

        [HttpPost("messagereceived"), UserAuthorization]
        public async Task<IActionResult> MessageReceived([FromBody] Message msg)
        {
            msg.TimeSent = DateTime.Now;
            await messageHub.Clients.User(msg.ReceiverUserId).ReportNewMessagesAsync("mensagem recebida", msg);
            await msgSvc.SendMessageAsync(msg);
            return Ok(msg);
        }
        [HttpDelete("{messagecode}"), UserAuthorization]
        public async Task<IActionResult> Delete(string messagecode)
        {
            var serverMsg = await msgSvc.GetMessageAsync(messagecode);
            
            if (serverMsg == null)
                return UnprocessableEntity($"Identificador inválido {messagecode} ou não existe");

            await msgSvc.DeleteMessageAsync(messagecode);

            return Ok();
        }
    }
}