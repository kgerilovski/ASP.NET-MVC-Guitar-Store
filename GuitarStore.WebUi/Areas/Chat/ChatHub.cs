using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace GuitarStore.WebUi.Areas.Chat
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            Clients.All.user(Context.User.Identity.Name);
            return base.OnConnected();
        }
        
        public void Send(string message)
        {
            Clients.Caller.message("You: " + message);
            Clients.Others.message(Context.User.Identity.Name + ": " + message);
        }
    }
}