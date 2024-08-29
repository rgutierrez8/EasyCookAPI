using EasyCookAPI.Models;

namespace EasyCookAPI.Core.Interfaces
{
    public interface ICommentService
    {
        public List<Comment> GetComments(int recipeId);
    }
}
