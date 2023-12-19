using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Food;
using Test12.Models.Models.ReadyFood;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IReadyFoodRepository : IRepository<ReadyProducts>
    {
        void Update(ReadyProducts obj);
        int GetLastStepId();

    }
   
}
