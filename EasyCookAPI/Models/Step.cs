using System.ComponentModel.DataAnnotations;

namespace EasyCookAPI.Models
{
    public class Step
    {
        [Key]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int NumberStep { get; set; }
        [Required]
        public string Describe { get; set; }
    }
}
