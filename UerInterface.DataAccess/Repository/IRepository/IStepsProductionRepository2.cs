using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Production;

namespace Test12.DataAccess.RepositoryPro.IRepositoryPro1
{
    public interface IStepsProductionRepository2 : IRepository<ProductionSteps>
    {

        void Update(ProductionSteps obj);
        void Delete(ProductionSteps obl);
        int GetLastStepId();

    }
}
