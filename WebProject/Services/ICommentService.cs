namespace WebProject.Services
{
    public interface ICommentService
    {
        Task SendCommentAsync(string comment, int numComments);
    }
}
