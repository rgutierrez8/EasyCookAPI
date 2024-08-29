using EasyCookAPI.Models;

namespace EasyCookAPI.Core.Interfaces
{
    public interface IStepService
    {
        public List<Step> GetSteps(int recipeId);
    }
}
