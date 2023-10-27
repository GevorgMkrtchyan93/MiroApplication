using Microsoft.AspNetCore.SignalR;

namespace Miro.Server.Services
{
    public class MiroHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
