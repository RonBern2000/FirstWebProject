namespace WebProject.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public int AnimalId { get; set; }
        public virtual Animal? Animal { get; set; }
    }
}
