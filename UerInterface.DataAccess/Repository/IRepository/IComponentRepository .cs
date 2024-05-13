using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IComponentRepository : IRepository<PreparationIngredients>
    {

        void Update(PreparationIngredients obj);
        int GetLastComponentId();



    }
}
