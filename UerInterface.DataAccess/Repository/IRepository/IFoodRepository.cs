using Test12.Models.Models.Food;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IFoodRepository : IRepository<FoodStuffs>
    {
        void Update(FoodStuffs obj);
        int GetLastStepId();

    }

}
