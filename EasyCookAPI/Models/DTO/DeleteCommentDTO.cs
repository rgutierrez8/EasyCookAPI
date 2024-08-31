namespace EasyCookAPI.Models.DTO
{
    public class DeleteCommentDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
    }
}
