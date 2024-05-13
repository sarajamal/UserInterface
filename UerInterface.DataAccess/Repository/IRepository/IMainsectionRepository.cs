using Test12.Models.Models;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IMainsectionRepository : IRepository<MainSections>
    {

        void Update(MainSections obj);

    }
}
