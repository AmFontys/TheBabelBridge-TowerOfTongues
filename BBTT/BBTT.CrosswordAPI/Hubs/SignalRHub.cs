using Microsoft.AspNetCore.SignalR;

namespace BBTT.CrosswordAPI.Hubs;

public class SignalRHub : Hub
{
    public async Task SendMessage (string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
    public async Task SendGridUpdate (string gridUpdate)
    {
        await Clients.All.SendAsync("ReceiveGridUpdate", gridUpdate);
    }
    public async Task SendCrosswordUpdate (string crosswordUpdate)
    {
        await Clients.All.SendAsync("ReceiveCrosswordUpdate", crosswordUpdate);
    }

}
