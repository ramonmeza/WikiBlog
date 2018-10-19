using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WikiBlog.Hubs
{
    public class PostHub : Hub
    {
        // RPCs
        // This will be send to the server
        public async Task SendPost(string title, string author, string content)
        {
            // The server will respond with this to every client
            await Clients.All.SendAsync("ReceivePost", title, author, content);
        }
    }
}