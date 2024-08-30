using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Interfaces
{
    public interface IIngredientService
    {
        public List<Ingredient> GetIngredients(int recipeId);
        void NewIngredients(Ingredient ing);
    }
}
