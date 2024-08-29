using EasyCookAPI.Models;

namespace EasyCookAPI.Core.Interfaces
{
    public interface IFavService
    {
        public List<Fav> GetFavs(int userId);
        public Boolean InFavs(int recipeId, int userId);
    }
}
