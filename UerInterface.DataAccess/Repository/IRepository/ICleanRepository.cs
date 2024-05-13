using Test12.Models.Models.Clean;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface ICleanRepository : IRepository<Cleaning>
    {
        void Update(Cleaning obj);
        int GetLastStepId();
    }
}
