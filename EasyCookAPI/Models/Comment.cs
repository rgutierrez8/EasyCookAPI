using System.ComponentModel.DataAnnotations;

namespace EasyCookAPI.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int? UserId {  get; set; }
        public User? User { get; set; }
        public string DatePublish { get; set; }
        [Required]
        public string Describe { get; set; }
    }
}
