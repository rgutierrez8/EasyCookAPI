using EasyCookAPI.Core;
using EasyCookAPI.Core.Helpers;
using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EasyCookAPI.Core.Services
{
    public class RecipeService : Repository<Recipe>, IRecipeService
    {
        private readonly IMapper _mapper;
        private readonly IFavService _favService;
        public RecipeService(EasyCookContext context, IMapper mapper, IFavService favService) : base(context)
        {
            _mapper = mapper;
            _favService = favService;
        }

        public RecipeDTO newRecipe(NewRecipeDTO recipe)
        {
            var NewRecipe = _mapper.MapNewRecipeToRecipe(recipe);
            Create(NewRecipe);
            Save();

            var data = GetByTitle(recipe.Title);

            _mapper.MapNewIngredientsDTOToIngredients(recipe.Ingredients, data.Id);
            data.ListIngredients = recipe.Ingredients;

            _mapper.MapNewStepDTOTOStep(recipe.Steps, data.Id);
            data.ListSteps = recipe.Steps;

            return data;
        }
        public List<RecipesListDTO> GetAll(int order)
        {
            var data = GetAll(source => source.Include(r => r.User)).ToList();

            if (order == 1) { data = GetAll(source => source.Include(r => r.User)).OrderByDescending(source => source.Likes).ToList(); } // ORDEN DE MAS A MENOS LIKES
            if (order == 2) { data = GetAll(source => source.Include(r => r.User)).OrderBy(source => source.Likes).ToList(); } // ORDEN DE MENOS A MAS LIKES
            if (order == 3) { data = GetAll(source => source.Include(r => r.User)).OrderByDescending(source => source.NeededTime).ToList(); } // ORDEN DE MAS A MENOS TIEMPO
            if (order == 4) { data = GetAll(source => source.Include(r => r.User)).OrderBy(source => source.NeededTime).ToList(); }

            return _mapper.MapListRecipeToListRecipeDTO(data);
        }

        public RecipeDTO GetRecipe(int id)
        {
            var data = FindByCondition(source => source.Id == id).Include(u => u.User).FirstOrDefault();

            return _mapper.MapRecipeToRecipeDTO(data) ;
        }

        public RecipeDTO GetByTitle(string title)
        {
            var data = FindByCondition(source => source.Title.ToLower() == title.ToLower()).Include(u => u.User).FirstOrDefault();

            if(data != null)
            {
                return _mapper.MapRecipeToRecipeDTO(data);
            }

            return null;
        }

        public List<RecipesListDTO> GetRecipesByUser(int userId)
        {
            var data = GetAll(source => source.Include(u => u.User)).Where(source => source.User.Id == userId).ToList();
  
            return _mapper.MapListRecipeToListRecipeDTO(data);
        }

        public List<RecipesListDTO> GetFavsUser(int userId)
        {
            List<Recipe> list = new List<Recipe>();
            var favs = _favService.GetFavs(userId);

            foreach (var fav in favs)
            {
                var data = FindByCondition(source => source.Id == fav.RecipeId).Include(u => u.User).FirstOrDefault();
                
                if(data != null)
                {
                    list.Add(data);
                }
            }

            return _mapper.MapListRecipeToListRecipeDTO(list);
        }
    }
}
