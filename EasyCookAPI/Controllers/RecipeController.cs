using EasyCookAPI.Core.Helpers;
using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyCookAPI.Controllers
{
    [Authorize]
    [Route("Recipes")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;

        public RecipeController(IRecipeService recipeService, IMapper mapper, IHelper helper)
        {
            _recipeService = recipeService;
            _mapper = mapper;
            _helper = helper;
        }

        [HttpPost("New")]
        public IActionResult NewRecipe([FromBody] NewRecipeDTO newRecipeDTO)
        {
            try
            {
                var userId = _helper.DecodeJwt(_helper.GetToken());
                var exist = _recipeService.GetByTitle(newRecipeDTO.Title, userId);

                if (exist == null)
                {
                    return Ok(_recipeService.newRecipe(newRecipeDTO, userId));
                }

                return BadRequest("Ya existe una receta con es título");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
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
                var userId = _helper.DecodeJwt(_helper.GetToken());
                return _recipeService.GetRecipe(Id, userId) != null ? Ok(_recipeService.GetRecipe(Id, userId)) : NotFound();
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
                var userId = _helper.DecodeJwt(_helper.GetToken());
                var data = _recipeService.GetByTitle(title, userId);

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

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            try
            {
                var userId = _helper.DecodeJwt(_helper.GetToken());
                var op = _recipeService.DeleteRecipe(id, userId);

                if (op)
                {
                    return Ok("La receta se eliminó correctamente");
                }

                return NotFound("Error al eliminar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Favs/{userId}")]
        public IActionResult GetFavs()
        {
            try
            {
                var userId = _helper.DecodeJwt(_helper.GetToken());
                return Ok(_recipeService.GetFavsUser(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Favs/New")]
        public IActionResult NewFav([FromBody] FavDTO fav)
        {
            try
            {
                var userId = _helper.DecodeJwt(_helper.GetToken());
                fav.UserId = userId;
                _mapper.MapNewFavDTOToFav(fav);

                return Ok();
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
                var userId = _helper.DecodeJwt(_helper.GetToken());
                fav.UserId = userId;
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
