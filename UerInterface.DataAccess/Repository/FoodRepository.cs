using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Food;

namespace Test12.DataAccess.Repository
{
    public class FoodRepository : Repository<FoodStuffs>, IFoodRepository
    {
        private readonly ApplicationDbContext _context;
        public FoodRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(FoodStuffs obj)
        {

            //_context.Update(obj);

        }
     
    }
}
