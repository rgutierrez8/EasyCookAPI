using System.ComponentModel.DataAnnotations;

namespace EasyCookAPI.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        [Required]
        public string IngredientName {  get; set; }
        [Required]
        public string Amount { get; set; }
    }
}
