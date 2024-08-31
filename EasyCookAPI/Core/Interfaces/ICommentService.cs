using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Interfaces
{
    public interface ICommentService
    {
        public List<Comment> GetComments(int recipeId);
        void NewComment(Comment newComment);
        bool DeleteComment(DeleteCommentDTO comment);
    }
}
