using System.ComponentModel.DataAnnotations;

namespace EasyCookAPI.Models.DTO
{
    public class NewUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string? Pic { get; set; }
        public string? Banner { get; set; }
    }
}
