namespace EasyCookAPI.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? Pic { get; set; }
        public string? Banner { get; set; }
        public int CountRecipes { get; set; }
        public List<RecipesListDTO> Recipes {  get; set; }
    }
}
