using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OdontoApp.Services;

namespace OdontoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController: ControllerBase
    {
        private readonly IHubContext<NotificationHubService> hubcontext;

    }
}