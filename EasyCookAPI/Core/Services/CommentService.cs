using EasyCookAPI.Core.Helpers;
using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EasyCookAPI.Core.Services
{
    public class CommentService : Repository<Comment>, ICommentService
    {
        public CommentService(EasyCookContext context) : base(context) 
        {
        }
        public List<Comment> GetComments(int recipeId)
        {
            var data = GetAll().Include(u => u.User).Where(source => source.RecipeId == recipeId).ToList();

            return data;
        }

        public void NewComment(Comment newComment)
        {
            Create(newComment);
            Save();
        }
        public bool DeleteComment(DeleteCommentDTO comment)
        {
            var data = FindByCondition(source => source.Id == comment.Id && source.RecipeId == comment.RecipeId && source.UserId == comment.UserId).FirstOrDefault();

            if (data != null)
            {
                Delete(data);
                Save();
                return true;
            }
            return false;
        }
    }
}
