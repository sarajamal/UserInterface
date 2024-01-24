using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IComponentRepository : IRepository<PreparationIngredients>
    {

        void Update(PreparationIngredients obj);
        int GetLastComponentId();



    }
}
