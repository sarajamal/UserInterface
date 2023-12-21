using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Food;
using Test12.Models.Models.ReadyFood;

namespace Test12.DataAccess.Repository
{
    public class ReadyFoodRepository : Repository<ReadyProducts>, IReadyFoodRepository
    {
        private readonly ApplicationDbContext _context;
        public ReadyFoodRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ReadyProducts obj)
        {

            //_context.Update(obj);

        }
        public int GetLastStepId()
        {
            // If there are no entries in the table, return 0 or an appropriate default value
            if (!_context.ReadyProducts.Any())
            {
                return 0;
            }
            // Retrieve and return the max PrepStepsID
            return _context.ReadyProducts.Max(p => p.ReadyProductsID);
        }

    }
}
