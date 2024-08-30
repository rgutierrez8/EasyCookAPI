using EasyCookAPI.Core.Helpers;
using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyCookAPI.Controllers
{
    [Route("Recipes")]
    [Controller]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeService recipeService, IMapper mapper)
        {
            _recipeService = recipeService;
            _mapper = mapper;
        }

        [HttpPost("New")]
        public IActionResult NewRecipe([FromBody] NewRecipeDTO newRecipeDTO)
        {
            try
            {
                var exist = _recipeService.GetByTitle(newRecipeDTO.Title);

                if (exist == null)
                {
                    return Ok(_recipeService.newRecipe(newRecipeDTO));
                }

                return BadRequest("Ya existe una receta con es título");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("All")]
        public IActionResult GetAll([FromQuery] int order = 0)
        {
            try
            {
                return Ok(_recipeService.GetAll(order));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            try
            {
                return _recipeService.GetRecipe(Id) != null ? Ok(_recipeService.GetRecipe(Id)) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Title/{title}")]
        public IActionResult GetByTitle(string title)
        {
            try
            {
                var data = _recipeService.GetByTitle(title);

                if (data != null)
                {
                    return Ok(data);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteRecipe(int id)
        {
            try
            {
                _recipeService.DeleteRecipe(id);
                return Ok("La receta se eliminó correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Favs/{userId}")]
        public IActionResult GetFavs(int userId)
        {
            try
            {
                return Ok(_recipeService.GetFavsUser(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Favs/Delete")]
        public IActionResult DeleteFav([FromBody] FavDTO fav)
        {
            try
            {
                _mapper.MapFavDTOToDelete(fav);
                return Ok("El favorito se eliminó correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
