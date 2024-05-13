using Test12.Models.Models.Clean;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IStepsCleanRepository3 : IRepository<CleaningSteps>
    {

        void Update(CleaningSteps obj);
        int GetLastStepId();

    }
}
