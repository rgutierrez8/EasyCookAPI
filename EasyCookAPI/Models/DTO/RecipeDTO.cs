namespace EasyCookAPI.Models.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Describe { get; set; }
        public List<IngredientsDTO> ListIngredients { get; set; }
        public List<StepDTO> ListSteps { get; set; }
        public string? MainImage { get; set; }
        public string? Img2 { get; set; }
        public string? Img3 { get; set; }
        public string? Img4 { get; set; }
        public string NeededTime { get; set; }
        public string Username { get; set; }
        public int Likes { get; set; }
        public int dontLike { get; set; }
        public List<CommentDTO> CommentList { get; set; }
        public bool InFav { get; set; }
    }
}
