using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Comment
    {
        
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Please enter a valid comment (max 255 letters)")]
        [DataType(DataType.MultilineText)]
        [StringLength(255)]
        public string? CommentText { get; set; }

        public int AnimalId { get; set; }

        public virtual Animal? Animal { get; set; }
    }
}
