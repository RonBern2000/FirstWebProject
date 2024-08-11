using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class UserModel
    {
        //public int UserModelId { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
