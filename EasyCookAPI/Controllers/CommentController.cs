using EasyCookAPI.Core.Helpers;
using EasyCookAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyCookAPI.Controllers
{
    [Route("Comment")]
    [Controller]
    public class CommentController : Controller
    {
        private readonly IMapper _mapper;

        public CommentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("New")]
        public IActionResult NewComment([FromBody] NewCommentDTO newCommentDTO)
        {
            try
            {
                var comments = _mapper.MapNewCommentDTOToComment(newCommentDTO);

                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
