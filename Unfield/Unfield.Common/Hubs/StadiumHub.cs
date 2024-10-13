using Microsoft.AspNetCore.SignalR;

namespace Unfield.Common.Hubs;

public class StadiumHub : Hub
{
    public async Task JoinStadium(string stadiumGroupName) => 
        await Groups.AddToGroupAsync(Context.ConnectionId, stadiumGroupName);

    public async Task LeaveStadium(string stadiumGroupName) => 
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, stadiumGroupName);
}