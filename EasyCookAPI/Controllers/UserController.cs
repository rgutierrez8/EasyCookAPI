using EasyCookAPI.Core.Helpers;
using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyCookAPI.Controllers
{
    [Authorize]
    [Route("/User")]
    [ApiController]
    [Controller]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHelper _helper;
        public UserController(IUserService userService, IHelper helper) 
        {
            _userService = userService;
            _helper = helper;
        }

        [HttpGet("Logged")]
        public IActionResult GetUserData()
        {
            try
            {
                var userId = _helper.DecodeJwt(_helper.GetToken());
                if (_userService.GetUser(userId) != null)
                {
                    return Ok(_userService.GetUser(userId));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [AllowAnonymous]
        [HttpPost("New")]
        public async Task<IActionResult> NewUser(NewUserDTO user)
        {
            try
            {
                var exist = _userService.UserExist(user.Email, user.Username);

                if(!exist)
                {
                    if(user.Banner != null)
                        user.Banner = await _helper.UploadImg(user.Banner);
                    if(user.Pic != null)
                        user.Pic = await _helper.UploadImg(user.Pic);

                    _userService.NewUser(user);
                    return Ok();
                }

                return Forbid("El usuario o email ya está registrado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("Update")]
        public IActionResult UpdatePass([FromBody] UpdateUserPassDTO update)
        {
            try
            {
                _userService.UpdatePass(update);
                return Ok("Password actualizado");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
