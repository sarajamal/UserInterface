using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.trade_mark;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface ITredMarketRepository : IRepository<Brands>
    {

        void Update(Brands obj);

    }
   
}
