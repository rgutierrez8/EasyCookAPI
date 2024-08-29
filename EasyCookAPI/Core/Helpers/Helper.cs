using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Helpers
{
    public interface IHelper
    {
        public RecipeDTO GetRecipeDTO(Recipe recipe);
    }

    public class HelperService : IHelper
    {
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IIngredientService _ingredientService;
        private readonly IStepService _stepService;
        private readonly IFavService _favService;

        public HelperService(IUserService userService, ICommentService commentService, IIngredientService ingredientService,
                            IStepService stepService, IFavService favService) 
        {
            _userService = userService;
            _commentService = commentService;
            _ingredientService = ingredientService;
            _stepService = stepService;
            _favService = favService;
        }

        public RecipeDTO GetRecipeDTO(Recipe recipe)
        {
            RecipeDTO recipeDTO = new RecipeDTO();
            //{
            //    Id = recipe.Id,
            //    Title = recipe.Title,
            //    Description = recipe.Describe,
            //    ListIngredients = _ingredientService.GetIngredients(recipe.Id),
            //    ListSteps = _stepService.GetSteps(recipe.Id),
            //    MainImage = recipe.MainImage,
            //    Img2 = recipe.Img2,
            //    Img3 = recipe.Img3,
            //    Img4 = recipe.Img4,
            //    Time = recipe.NeededTime + " Min",
            //    //Username = "By " + _userService.GetUser(recipe.UserId).Username,
            //    Like = recipe.Likes,
            //    dontLike = recipe.DontLike,
            //    CommentList = _commentService.GetComments(recipe.Id),
            //    InFav = _favService.InFavs(recipe.Id, _userService.GetId("winnions"))
            //};

            return recipeDTO;
        }
    }
}
