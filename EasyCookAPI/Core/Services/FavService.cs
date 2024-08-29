using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;

namespace EasyCookAPI.Core.Services
{
    public class FavService : Repository<Fav>, IFavService
    {
        public FavService(EasyCookContext context) : base(context) { }

        public List<Fav> GetFavs(int userID)
        {
            var data = GetAll().Where(source => source.UserId == userID).ToList();

            return data;
        }
        public bool InFavs(int recipeId, int userId)
        {
            var data = FindByCondition(source => source.RecipeId == recipeId && source.UserId == userId).FirstOrDefault();

            return data != null ? true : false;
        }
    }
}
