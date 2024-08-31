using EasyCookAPI.Core.Helpers;
using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyCookAPI.Controllers
{
    [Authorize]
    [Route("Comment")]
    [ApiController]
    [Controller]
    public class CommentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IHelper _helper;
        private readonly ICommentService _commentService;

        public CommentController(IMapper mapper, IHelper helper, ICommentService commentService)
        {
            _mapper = mapper;
            _helper = helper;
            _commentService = commentService;
        }

        [HttpPost("New")]
        public IActionResult NewComment([FromBody] NewCommentDTO newCommentDTO)
        {
            try
            {
                var userId = _helper.DecodeJwt(_helper.GetToken());
                newCommentDTO.UserId = userId;

                var comments = _mapper.MapNewCommentDTOToComment(newCommentDTO);

                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteComment([FromBody] DeleteCommentDTO comment)
        {
            try
            {
                var userId = _helper.DecodeJwt(_helper.GetToken());
                comment.UserId = userId;
                var op = _commentService.DeleteComment(comment);

                if (op)
                {
                    return Ok("Comentario eliminado con exito");
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
