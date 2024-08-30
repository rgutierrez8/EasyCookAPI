using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Models;
using EasyCookAPI.Models.DTO;

namespace EasyCookAPI.Core.Services
{
    public class StepService : Repository<Step>, IStepService
    {
        public StepService(EasyCookContext context) : base(context) { }
        public List<Step> GetSteps(int recipeId)
        {
            var data =  GetAll().Where(source => source .RecipeId == recipeId).ToList();

            return data;
        }

        public void NewStep(Step step) 
        { 
            Create(step);
            Save();
        }
    }
}
