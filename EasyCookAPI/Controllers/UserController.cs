using EasyCookAPI.Core.Interfaces;
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
    }
}
