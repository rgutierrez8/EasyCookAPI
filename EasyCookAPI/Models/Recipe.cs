using System.ComponentModel.DataAnnotations;

namespace EasyCookAPI.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Describe { get; set; }
        public string? MainImage { get; set; }
        public string? Img2 { get; set; }
        public string? Img3 { get; set; }
        public string? Img4 { get; set; }
        public string NeededTime { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int Likes { get; set; }
        public int DontLike { get;set; }
    }
}
