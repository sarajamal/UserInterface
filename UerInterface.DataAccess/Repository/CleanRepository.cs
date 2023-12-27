using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Clean;
using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository
{
    public class CleanRepository : Repository<Cleaning>, ICleanRepository
    {

        private readonly ApplicationDbContext _context;
        public CleanRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Cleaning obj)
        {
            var objFormDb = _context.Cleaning.FirstOrDefault(u => u.BrandFK == obj.BrandFK);
            if (objFormDb != null)
            {

                objFormDb.DeviceName = obj.DeviceName;

                _context.SaveChanges();
            }
        }

        public int GetLastStepId()
        {
            // If there are no entries in the table, return 0 or an appropriate default value
            if (!_context.CleaningSteps.Any())
            {
                return 0;
            }
            // Retrieve and return the max PrepStepsID
            return _context.CleaningSteps.Max(p => p.CleaStepsID);
             

        }

    }
}

