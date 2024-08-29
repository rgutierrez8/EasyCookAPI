using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Services
{
    public class IngredientsService : Repository<Ingredient>, IIngredientService
    {
        public IngredientsService(EasyCookContext context) : base(context) { }
        public List<Ingredient> GetIngredients(int recipeId)
        {
            var data = GetAll().Where(source => source.RecipeId == recipeId).ToList();
            return data;
        }
    }
}
