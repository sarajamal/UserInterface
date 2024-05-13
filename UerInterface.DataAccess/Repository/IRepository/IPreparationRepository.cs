using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IPreparationRepository : IRepository<Preparations>
    {
        void Update(Preparations obj);

    }

}
