using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Food;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IFoodRepository : IRepository<FoodStuffs>
    {
        void Update(FoodStuffs obj);
        int GetLastStepId();

    }
    
}
