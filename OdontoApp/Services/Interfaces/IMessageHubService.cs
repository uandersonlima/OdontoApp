using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services.Interfaces
{
    public interface IMessageHubService
    {
        /*WebSockets*/
        Task ReportNewMessagesAsync(string title, Message msg);
        Task ReportReceivingMessagesAsync(string title, Message msg);
        Task UserIsConnectedAsync(string id);
        Task UserIsDisconnectedAsync(string id);

    }
}