using System.ComponentModel.DataAnnotations;

namespace EasyCookAPI.Models.DTO
{
    public class NewCommentDTO
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string Describe { get; set; }
    }
}
