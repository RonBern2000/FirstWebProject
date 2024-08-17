using Microsoft.AspNetCore.SignalR;
using WebProject.Hubs;

namespace WebProject.Services
{
    public class CommentService : ICommentService
    {
        private readonly IHubContext<MainHub> _hubContext;
        public CommentService(IHubContext<MainHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendCommentAsync(string comment, int numComments)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveComment", comment, numComments);
        }
    }
}
