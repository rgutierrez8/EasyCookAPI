using EasyCookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyCookAPI
{
    public class EasyCookContext : DbContext
    {
        public EasyCookContext(DbContextOptions<EasyCookContext> options) : base(options) { }

        DbSet<User> Users { get; set; }
        DbSet<Step> Steps { get; set; }
        DbSet<Recipe> Recipes { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<Fav> Favs { get; set; }
        DbSet<Comment> Comments { get; set; }
    }
}
