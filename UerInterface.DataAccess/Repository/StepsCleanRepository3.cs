using Microsoft.EntityFrameworkCore;
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
    public class StepsCleanRepository3 : Repository<CleaningSteps>, IStepsCleanRepository3
    {
        private readonly ApplicationDbContext _context;
        public StepsCleanRepository3(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(CleaningSteps obj)
        {
            var objFormDb = _context.CleaningSteps.FirstOrDefault(u => u.CleaStepsID == obj.CleaStepsID);
            if (objFormDb != null)
            {
                // Update properties for Step 1
                objFormDb.CleaText = obj.CleaText;

                if (obj.CleaStepsImage != null)
                {
                    objFormDb.CleaStepsImage = obj.CleaStepsImage;
                }
                // Update properties for Step 2
                
                // Save changes to the database
                _context.Entry(objFormDb).State = EntityState.Modified;
                _context.SaveChanges();

                //_context.Update(obj);

            }
        }
    }
}
