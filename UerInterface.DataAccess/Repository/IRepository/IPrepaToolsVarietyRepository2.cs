using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Production;

namespace Test12.DataAccess.RepositoryPro.IRepositoryPro1
{
    public interface IPrepaToolsVarietyRepository2 : IRepository<ProductionTools>
    {

        void Update(ProductionTools obj);



    }
   
}
