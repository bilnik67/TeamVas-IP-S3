using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;

namespace TeamVas.API.Middleware.Chat
{
    public class MessageHub : Hub<IChatClient>
    {
        public async Task SendMessage(MessageModel message)
        {
            await Clients.All.ReceiveMessage(message);
        }
    }
}
