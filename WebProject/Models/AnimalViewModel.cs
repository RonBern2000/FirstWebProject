using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class AnimalViewModel // A model to get data from the user.
    {
        [Required(ErrorMessage = "Please enter a valid Name")]
        public string? Name { get; set; }
        public int Age { get; set; }
        public IFormFile? Picture { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
    }
}
