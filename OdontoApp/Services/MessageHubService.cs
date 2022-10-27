using Microsoft.AspNetCore.SignalR;
using OdontoApp.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class MessageHubService : Hub<IMessageHubService>
    {
        private readonly IMessageService msgService;

        public MessageHubService(IMessageService msgService)
        {
            this.msgService = msgService;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.UserIsConnectedAsync($"{Context.UserIdentifier} se conectou");
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.UserIsDisconnectedAsync($"{Context.UserIdentifier} se desconectou");
        }
    }
}
