using System.ComponentModel.DataAnnotations;

namespace ZooLib.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
