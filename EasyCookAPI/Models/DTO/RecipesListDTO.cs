namespace EasyCookAPI.Models.DTO
{
    public class RecipesListDTO
    {
		public int Id { get; set; }
		public string Title { get; set; }
		public string? MainImage { get; set; }
		public string NeededTime {  get; set; }
		public string Username {  get; set; }
		public int Likes {  get; set; }
		public int dontLike { get; set; }
		public string TimeToCompare { get; set; }
    }
}
