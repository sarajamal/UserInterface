using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.trade_mark;

namespace Test12.DataAccess.Repository
{
    public class TredMarketRepository : Repository<Brands>, ITredMarketRepository
    {
        private readonly ApplicationDbContext _context;
        public TredMarketRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Brands obj)
        {
           
            //_context.Update(obj);

        }


    }
    
}
