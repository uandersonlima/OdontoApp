using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> GetMessageAsync(string messagecode, string senderId);
        Task<List<Message>> GetMessagesAsync(AppView appview, string senderId);
        Task<List<Message>> GetUnreadMessagesAsync(string senderId);
        Task DeleteMessageAsync(Message msg);
        Task SendMessageAsync(Message msg);
        Task UpdateMessageAsync(Message msg);
        Task<List<string>> UserListAsync(string userId);
    }
}
