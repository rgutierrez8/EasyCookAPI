using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Interfaces
{
    public interface IFavService
    {
        public List<Fav> GetFavs(int userId);
        public Boolean InFavs(int recipeId, int userId);
        void NewFav(Fav newFav);
        void DeleteFav(FavDTO fav);
    }
}
