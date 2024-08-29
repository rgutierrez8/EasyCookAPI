using System.ComponentModel.DataAnnotations;

namespace EasyCookAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Pass { get; set; }
        public string? Pic {  get; set; }
        public string? Banner { get; set; }

    }
}
