using Microsoft.AspNetCore.SignalR;

namespace Miro.Server.Services
{
    public class MiroHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendDrawingCommand(double x1, double y1, double x2, double y2)
        {
            await Clients.Others.SendAsync("ReceiveDrawingCommand", x1, y1, x2, y2);
        }
    }
}
