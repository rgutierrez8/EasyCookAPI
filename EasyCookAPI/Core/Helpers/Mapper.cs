using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Helpers
{
    public interface IMapper
    {
        #region USER IMAPPER
        public UserDTO MapUserToUserDTO(User user, List<RecipesListDTO> recipes);
        LogedUserDTO MapUserTOLogedUserDTO(User user);
        User MapNewUserToUser(NewUserDTO newUser);

        #endregion

        #region RECIPE IMAPPER
        Recipe MapNewRecipeToRecipe(NewRecipeDTO newRecipeDTO, int userId);
        List<RecipesListDTO> MapListRecipeToListRecipeDTO(List<Recipe> recipes);
        RecipeDTO MapRecipeToRecipeDTO(Recipe recipe, int userId);

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
        void MapNewFavDTOToFav(FavDTO newFavDTO);
        void MapFavDTOToDelete(FavDTO favDTO);

        #endregion
    }

    public class Mapper : IMapper
    {
        private readonly IIngredientService _ingredientService;
        private readonly IStepService _stepService;
        private readonly ICommentService _commentService;
        private readonly IFavService _favService;
        private readonly IHelper _helper;

        public Mapper(IIngredientService ingredientService, IStepService stepService, ICommentService commentService, IFavService favService, IHelper helper) 
        {
            _ingredientService = ingredientService;
            _stepService = stepService;
            _commentService = commentService;
            _favService = favService;
            _helper = helper;
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

        public LogedUserDTO MapUserTOLogedUserDTO(User user)
        {
            LogedUserDTO userDTO = new LogedUserDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Token = _helper.GenerarToken(user)
            };
            return userDTO;
        }

        public User MapNewUserToUser(NewUserDTO newUser)
        {
            User user = new User()
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Username = newUser.Username,
                Pass = _helper.EncryptPassSha25(newUser.Pass),
                Banner = newUser.Banner,
                Pic = newUser.Pic
            };
            return user;
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

        public RecipeDTO MapRecipeToRecipeDTO(Recipe recipe, int userId)
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
                InFav = _favService.InFavs(recipe.Id, userId), 
            };

            return recipeDTO;
        }

        public Recipe MapNewRecipeToRecipe(NewRecipeDTO newRecipeDTO, int userId)
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
                UserId = userId,
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
                UserId = newCommentDTO.UserId,
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
        public void MapNewFavDTOToFav(FavDTO newFavDTO)
        {
            Fav fav = new Fav()
            {
                RecipeId = newFavDTO.RecipeId,
                UserId = newFavDTO.UserId,
            };

            _favService.NewFav(fav);
        }
        public void MapFavDTOToDelete(FavDTO favDTO)
        {
            _favService.DeleteFav(favDTO);
        }

        #endregion
    }
}
