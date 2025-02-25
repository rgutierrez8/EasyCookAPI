using Microsoft.AspNetCore.Http;

namespace EasyCookAPI.Models.DTO
{
    public class NewRecipeDTOIMG
    {
        public string Title { get; set; }
        public string Describe { get; set; }
        public string NeededTime { get; set; }
        public IFormFile? MainImage { get; set; }
        public IFormFile? Img2 { get; set; }
        public IFormFile? Img3 { get; set; }
        public IFormFile? Img4 { get; set; }
        public List<IngredientsDTO> Ingredients { get; set; }
        public List<StepDTO> Steps { get; set; }
    }
}
