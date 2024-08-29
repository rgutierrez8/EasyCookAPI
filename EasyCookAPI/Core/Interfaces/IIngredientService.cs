using EasyCookAPI.Models;

namespace EasyCookAPI.Core.Interfaces
{
    public interface IIngredientService
    {
        public List<Ingredient> GetIngredients(int recipeId);
    }
}
