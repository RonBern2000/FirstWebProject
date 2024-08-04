using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebProject.Validators;

namespace WebProject.Models
{
    public class AnimalModelEditor
    {
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(50, ErrorMessage = "Please do not enter a name over 50 characters")]
        [DisplayName("Animal name:")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter an age")]
        [Range(1, 250, ErrorMessage = "Please enter an age between 1-250")]
        [DisplayName("Animal age:")]
        public int Age { get; set; }

        [DisplayName("Animal picture:")]
        public IFormFile? Picture { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(999, ErrorMessage = "Please do not enter a name over 999 characters")]
        [DisplayName("Animal's description:")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        [DisplayName("Animal's category:")]
        public string? CategoryName { get; set; }
    }
}
