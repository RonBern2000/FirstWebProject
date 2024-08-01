using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Please enter a valid Name")]
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? PictureName { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
