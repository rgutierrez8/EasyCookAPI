using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyCookAPI.Controllers
{
    [Route("/User")]
    [Controller]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet("Id/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                if(_userService.GetUser(id) != null)
                {
                    return Ok(_userService.GetUser(id));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Username/{username}")]
        public IActionResult GetByUsername(string username)
        {
            try
            {
                var data = _userService.GetId(username);
                if (data != -1)
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

        [HttpPost("New")]
        public IActionResult NewUser(NewUserDTO user)
        {
            try
            {
                _userService.NewUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDTO user)
        {
            try
            {
                var data = _userService.Login(user);
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound("Usuario inexistente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
