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
    public class StepsCleanRepository3 : Repository<الخطوات3>, IStepsCleanRepository3
    {
        private readonly ApplicationDbContext _context;
        public StepsCleanRepository3(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(الخطوات3 obj)
        {
            var objFormDb = _context.الخطوات3.FirstOrDefault(u => u.ID == obj.ID);
            if (objFormDb != null)
            {
                // Update properties for Step 1
                objFormDb.الخطوة1 = obj.الخطوة1;

                if (obj.الصورة1 != null)
                {
                    objFormDb.الصورة1 = obj.الصورة1;
                }
                // Update properties for Step 2
                objFormDb.الخطوة2 = obj.الخطوة2;

                if (obj.الصورة2 != null)
                {
                    objFormDb.الصورة2 = obj.الصورة2;
                }
                // Save changes to the database
                _context.Entry(objFormDb).State = EntityState.Modified;
                _context.SaveChanges();

                //_context.Update(obj);

            }
        }
    }
}
