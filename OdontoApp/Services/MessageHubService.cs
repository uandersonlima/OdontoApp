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
            var a = Context.UserIdentifier.ToString();
            var userList = await msgService.UserListAsync();
            userList.Add(a);
            await Clients.Users(userList).UserIsConnectedAsync($"{a} se conectou");
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var a = Context.UserIdentifier.ToString();
            var userList = await msgService.UserListAsync();
            userList.Add(a);
            await Clients.Users(userList).UserIsDisconnectedAsync($"{a} se desconectou");
        }
    }
}
