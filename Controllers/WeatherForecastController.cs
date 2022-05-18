using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signal_r.Controllers
{
    public class MainHub : Hub
    {
        private static readonly Dictionary<string, string> ConnectionsGroup = new Dictionary<string, string>();


        public async Task JoinGroup(string group)
        {
            if (ConnectionsGroup.ContainsKey(Context.ConnectionId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, ConnectionsGroup[Context.ConnectionId]);
                ConnectionsGroup.Remove(Context.ConnectionId);
            }

            ConnectionsGroup.Add(Context.ConnectionId, group);
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task NewMessage(string groupName, string message, string userName)
        {
            await Clients.Group(groupName).SendAsync("newMessage", message, userName);
        }
    }

}
