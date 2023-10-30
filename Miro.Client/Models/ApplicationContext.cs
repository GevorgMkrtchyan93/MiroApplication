using Microsoft.AspNetCore.SignalR.Client;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Miro.Client.Models
{
    public class ApplicationContext
    {
        public static ApplicationContext Instance { get; private set; } = new ApplicationContext();

        public string Username { get; set; }

        public int SelectedBoardId { get; set; }

        public int CurrentBoardId { get; set; }

        public HubConnection Connection { get; set; }

        public event Action<double, double, double, double> ReceivedDrawingEvent;

        public async void Connect()
        {
            // Create a SignalR connection
            Connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7108/MiroHub") // Replace with the URL of your SignalR hub
                .Build();

            // Define event handlers for receiving messages
            Connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                // Handle the received message here
                // Update your UI or perform any necessary actions
            });

            Connection.On<double, double, double, double>("ReceiveDrawingCommand", (receivedX1, receivedY1, receivedX2, receivedY2) =>
            {
                ReceivedDrawingEvent?.Invoke(receivedX1, receivedY1, receivedX2, receivedY2);
            });

            try
            {
                await Connection.StartAsync();
                // Connection established
            }
            catch (Exception ex)
            {
                // Handle connection error
            }
        }

        public async Task SendAsync()
        {
            await Connection.InvokeAsync("SendMessage", Username, "Test");
        }

        public async Task SendDrawingCommand(double x1, double y1, double x2, double y2)
        {
            await Connection.InvokeAsync("SendDrawingCommand", x1, y1, x2, y2);
        }
    }
}