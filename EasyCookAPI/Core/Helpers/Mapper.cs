using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Helpers
{
    public interface IMapper
    {
        #region USER IMAPPER
        public UserDTO MapUserToUserDTO(User user, List<RecipesListDTO> recipes);

        #endregion

        #region RECIPE IMAPPER
        Recipe MapNewRecipeToRecipe(NewRecipeDTO newRecipeDTO);
        List<RecipesListDTO> MapListRecipeToListRecipeDTO(List<Recipe> recipes);
        RecipeDTO MapRecipeToRecipeDTO(Recipe recipe);

        #endregion

        #region INGREDIENT IMAPPER

        List<IngredientsDTO> MapIngredientsToIngredientsDTO(List<Ingredient> ingredients);
        void MapNewIngredientsDTOToIngredients(List<IngredientsDTO> ingredients, int recipeId);

        #endregion

        #region STEP IMAPPER

        void MapNewStepDTOTOStep(List<StepDTO> stepDTO, int recipeId);
        List<StepDTO> MapStepToListStepDTO(List<Step> steps);

        #endregion

        #region COMMENT IMAPPER

        List<CommentDTO> MapNewCommentDTOToComment(NewCommentDTO newCommentDTO);
        List<CommentDTO> MapCommentsToListCommentDTO(List<Comment> comments);

        #endregion

        #region FAV IMAPPER

        List<RecipesListDTO> MapFavToListRecipeDTO(List<Fav> favList);

        #endregion
    }

    public class Mapper : IMapper
    {
        private readonly IIngredientService _ingredientService;
        private readonly IStepService _stepService;
        private readonly ICommentService _commentService;
        private readonly IFavService _favService;

        public Mapper(IIngredientService ingredientService, IStepService stepService, ICommentService commentService, IFavService favService) 
        {
            _ingredientService = ingredientService;
            _stepService = stepService;
            _commentService = commentService;
            _favService = favService;
        }

        #region USER MAPPER
        public UserDTO MapUserToUserDTO(User user, List<RecipesListDTO> recipes)
        {
            UserDTO userDTO = new UserDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Pic = user.Pic,
                Banner = user.Banner,
                CountRecipes = recipes.Count,
                Recipes =  recipes
            };

            return userDTO;
        }


        #endregion

        #region RECIPE MAPPER
        public List<RecipesListDTO> MapListRecipeToListRecipeDTO(List<Recipe> recipes)
        {
            List<RecipesListDTO> recipesListDTOs = new List<RecipesListDTO>();

            foreach (Recipe recipe in recipes)
            {
                RecipesListDTO recipeDTO = new RecipesListDTO()
                {
                    Id = recipe.Id,
                    Title = recipe.Title,
                    MainImage = recipe.MainImage,
                    NeededTime = recipe.NeededTime,
                    Username = recipe.User.Username,
                    Likes = recipe.Likes,
                    dontLike = recipe.DontLike,
                    TimeToCompare = recipe.NeededTime
                };

                recipesListDTOs.Add(recipeDTO);
            }
            return recipesListDTOs;
        }

        public RecipeDTO MapRecipeToRecipeDTO(Recipe recipe)
        {
            RecipeDTO recipeDTO = new RecipeDTO()
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Describe = recipe.Describe,
                ListIngredients = MapIngredientsToIngredientsDTO(_ingredientService.GetIngredients(recipe.Id)),
                ListSteps = MapStepToListStepDTO(_stepService.GetSteps(recipe.Id)),
                MainImage = recipe.MainImage,
                Img2 = recipe.Img2,
                Img3 = recipe.Img3,
                Img4 = recipe.Img4,
                NeededTime = recipe.NeededTime,
                Username = recipe.User.Username,
                Likes = recipe.Likes,
                dontLike = recipe.DontLike,
                CommentList = MapCommentsToListCommentDTO(_commentService.GetComments(recipe.Id)),
                InFav = _favService.InFavs(recipe.Id, 6), // <-- HARDOCDEADO EL 6 HASTA TENER EL USUARIO LOGEADO
            };

            return recipeDTO;
        }

        public Recipe MapNewRecipeToRecipe(NewRecipeDTO newRecipeDTO)
        {
            Recipe recipe = new Recipe()
            {
                Title = newRecipeDTO.Title,
                Describe = newRecipeDTO.Describe,
                NeededTime = newRecipeDTO.NeededTime,
                MainImage = newRecipeDTO.MainImage,
                Img2 = newRecipeDTO.Img2,
                Img3 = newRecipeDTO.Img3,
                Img4 = newRecipeDTO.Img4,
                UserId = 6, // <-- HARDCODEADA HASTA TENER EL USER LOGEADO 
                Likes = 0,
                DontLike = 0,
            };

            // =============================================================================================================
            // =                    FALTA CREAR LAS LISTAS DE INGRE STEPS PARA GUARDAR EN NUEVA RECETA                     =
            // =============================================================================================================

            return recipe;
        }

        #endregion

        #region INGREDIENT MAPPER

        public List<IngredientsDTO> MapIngredientsToIngredientsDTO (List<Ingredient> ingredients)
        {
            List<IngredientsDTO> ingredientsDTOs = new List<IngredientsDTO>();

            foreach(var ing in ingredients)
            {
                IngredientsDTO ingredient = new IngredientsDTO()
                {
                    Amount = ing.Amount,
                    IngredientName = ing.IngredientName,
                };

                ingredientsDTOs.Add(ingredient);   
            }

            return ingredientsDTOs;
        }

        public void MapNewIngredientsDTOToIngredients(List<IngredientsDTO> ingredients, int recipeId)
        {
            foreach (IngredientsDTO ingredient in ingredients)
            {
                Ingredient ingredient1 = new Ingredient()
                {
                    RecipeId = recipeId,
                    Amount = ingredient.Amount,
                    IngredientName = ingredient.IngredientName,
                };
                _ingredientService.NewIngredients(ingredient1);
            }
        }

        #endregion

        #region STEP MAPPER

        public void MapNewStepDTOTOStep(List<StepDTO> stepDTO, int recipeId)
        {
            foreach (StepDTO step in stepDTO)
            {
                Step step1 = new Step()
                {
                    RecipeId = recipeId,
                    NumberStep = step.NumberStep,
                    Describe = step.Describe,
                };

                _stepService.NewStep(step1);
            }
        }

        public List<StepDTO> MapStepToListStepDTO(List<Step> steps)
        {
            List<StepDTO> stepList = new List<StepDTO>();

            foreach (var step in steps)
            {
                StepDTO stepDTO = new StepDTO()
                {
                    Describe = step.Describe,
                    NumberStep = step.NumberStep,
                };
                stepList.Add(stepDTO);
            }

            return stepList;
        }

        #endregion

        #region COMMENT MAPPER

        public List<CommentDTO> MapCommentsToListCommentDTO(List<Comment> comments)
        {
            List<CommentDTO> ListCommentDTO = new List<CommentDTO>();

            foreach(Comment comment in comments)
            {
                CommentDTO commentDTO = new CommentDTO()
                {
                    Username = comment.User.Username,
                    DatePublish = comment.DatePublish,
                    Describe = comment.Describe,
                };
                ListCommentDTO.Add(commentDTO);
            }
            return ListCommentDTO;
        }

        public List<CommentDTO> MapNewCommentDTOToComment(NewCommentDTO newCommentDTO)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            Comment commentDTO = new Comment()
            {
                RecipeId = newCommentDTO.RecipeId,
                UserId = newCommentDTO.UserId, // <-- HARDOCDEADO HASTA TENER EL USUARIO LOGEADO
                DatePublish = dateTime.ToString("dd/MM/yyyy"),
                Describe = newCommentDTO.Describe
            };

            _commentService.NewComment(commentDTO);

            return MapCommentsToListCommentDTO(_commentService.GetComments(newCommentDTO.RecipeId));
        }

        #endregion

        #region FAV MAPPER

        public List<RecipesListDTO> MapFavToListRecipeDTO(List<Fav> favList)
        {
            List<RecipesListDTO> recipesListDTOs = new List<RecipesListDTO>();

            foreach(Fav fav in favList)
            {

            }

            return recipesListDTOs;
        }

        #endregion
    }
}
