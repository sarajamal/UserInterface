using Test12.Models.Models.ReadyFood;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IReadyFoodRepository : IRepository<ReadyProducts>
    {
        void Update(ReadyProducts obj);
        int GetLastStepId();

    }

}
