using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> GetMessageAsync(string messagecode);
        Task<List<Message>> GetMessagesAsync(AppView appview);
        Task<List<Message>> GetUnreadMessagesAsync();
        Task DeleteMessageAsync(Message msg);
        Task DeleteMessageAsync(string messagecode);
        Task SendMessageAsync(Message msg);
        Task UpdateMessageAsync(Message msg);
    }
}
