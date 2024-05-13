using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Production;

namespace Test12.DataAccess.RepositoryPro.IRepositoryPro1
{
    public interface IItemsREpository : IRepository<Production>
    {
        void Update(Production obj);

    }

}
