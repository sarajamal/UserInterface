using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Clean;
using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IStepsCleanRepository3 : IRepository<الخطوات3>
    {

        void Update(الخطوات3 obj);
     
    }
}
