using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ZooLib.Validators;

namespace ZooLib.Models
{
    public class AnimalViewModel // A model to get data from the user.
    {
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(50,ErrorMessage = "Please do not enter a name over 50 characters")]
        [DisplayName("Animal name:")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter an age")]
        [Range(1,250,ErrorMessage = "Please enter an age between 1-250")]
        [DisplayName("Animal age:")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter a picture file")]
        [DisplayName("Animal picture:")]
        [AllowedFileType([".jpg", ".jpeg", ".png"], ErrorMessage = "Wrong file type(extention)")]
        public IFormFile? Picture { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [DisplayName("Animal's description:")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        [DisplayName("Animal's category:")]
        public string? CategoryName { get; set; }
    }
}
