using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Interfaces
{
    public interface IRecipeService
    {
        public RecipeDTO GetRecipe (int id);
        public List<RecipesListDTO> GetAll(int order);
        public List<RecipesListDTO> GetRecipesByUser (int userId);
        public List<RecipesListDTO> GetFavsUser(int userId);
        RecipeDTO newRecipe(NewRecipeDTO recipe);
        RecipeDTO GetByTitle(string title);
    }
}
