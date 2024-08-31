using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Interfaces
{
    public interface IRecipeService
    {
        public RecipeDTO GetRecipe (int id, int userId);
        public List<RecipesListDTO> GetAll(int order);
        public List<RecipesListDTO> GetRecipesByUser (int userId);
        public List<RecipesListDTO> GetFavsUser(int userId);
        RecipeDTO newRecipe(NewRecipeDTO recipe, int userId);
        RecipeDTO GetByTitle(string title, int userId);
        bool DeleteRecipe(int id, int userId);
    }
}
