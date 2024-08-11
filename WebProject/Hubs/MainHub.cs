using Microsoft.AspNetCore.SignalR;
using WebProject.Services;

namespace WebProject.Hubs
{
    public class MainHub : Hub
    {
        private readonly ICommentService _commentService;
        public MainHub(ICommentService commentService)
        {
            _commentService = commentService;
        }
    }
}
