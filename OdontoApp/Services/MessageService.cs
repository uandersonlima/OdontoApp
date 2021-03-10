using OdontoApp.Models;
using OdontoApp.Models.Const;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepos;
        private readonly IAuthService authService;

        public MessageService(IMessageRepository messageRepos, IAuthService authService)
        {
            this.messageRepos = messageRepos;
            this.authService = authService;
        }

        /*Persist�ncia banco de dados */

        public async Task DeleteMessageAsync(Message msg)
        {
            await messageRepos.DeleteMessageAsync(msg);
        }

        public async Task DeleteMessageAsync(string messagecode)
        {
            await DeleteMessageAsync(await GetMessageAsync(messagecode));
        }

        public async Task<Message> GetMessageAsync(string messagecode)
        {
            return await messageRepos.GetMessageAsync(messagecode, authService.GetLoggedUserAsync().Result.Id);
        }

        public async Task<List<Message>> GetMessagesAsync(AppView appview)
        {
            appview.RecordPerPage ??= NumElement.NumElements;
            appview.NumberPag ??= 1;
            return await messageRepos.GetMessagesAsync(appview, authService.GetLoggedUserAsync().Result.Id);
        }
        public async Task<List<Message>> GetUnreadMessagesAsync()
        {
            return await messageRepos.GetUnreadMessagesAsync(authService.GetLoggedUserAsync().Result.Id);
        }
        public async Task SendMessageAsync(Message msg)
        {
            msg.Messagecode = Guid.NewGuid().ToString("N");
            await messageRepos.SendMessageAsync(msg);
        }

        public async Task UpdateMessageAsync(Message msg)
        {
            if (msg.SenderUserId == authService.GetLoggedUserAsync().Result.Id)
                await messageRepos.SendMessageAsync(msg);
        }

        public async Task<List<string>> UserListAsync()
        {
            return await messageRepos.UserListAsync(authService.GetLoggedUserAsync().Result.Id);
        }
    }
}