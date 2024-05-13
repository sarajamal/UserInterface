using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IStepsPreparationRepository : IRepository<PreparationSteps>
    {

        void Update(PreparationSteps obj);
        int GetLastStepId();



    }
}
