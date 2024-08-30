using EasyCookAPI.Models;

namespace EasyCookAPI.Core.Interfaces
{
    public interface IStepService
    {
        void NewStep(Step step);
        public List<Step> GetSteps(int recipeId);
    }
}
