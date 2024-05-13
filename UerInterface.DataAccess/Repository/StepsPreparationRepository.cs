
using Microsoft.EntityFrameworkCore;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository
{
    public class StepsPreparationRepository : Repository<PreparationSteps>, IStepsPreparationRepository
    {
        private readonly ApplicationDbContext _context;
        public StepsPreparationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(PreparationSteps obj)
        {
            var objFormDb = _context.PreparationSteps.FirstOrDefault(u => u.PrepStepsID == obj.PrepStepsID);
            if (objFormDb != null)
            {
                // Update properties for Step 1
                objFormDb.PrepText = obj.PrepText;

                if (obj.PrepImage != null)
                {
                    objFormDb.PrepImage = obj.PrepImage;
                }

                // Save changes to the database
                _context.Entry(objFormDb).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }

        // New function to get the last ID
        public int GetLastStepId()
        {
            // If there are no entries in the table, return 0 or an appropriate default value
            if (!_context.PreparationSteps.Any())
            {
                return 0;
            }

            // Retrieve and return the max PrepStepsID
            return _context.PreparationSteps.Max(p => p.PrepStepsID);
        }

        //public void Delete (الخطوات obj)
        //{
        //   var objFormDb = _context.الخطوات.FirstOrDefault(u => u.ID == obj.ID); 
        //    if(objFormDb != null)
        //    {

        //    }
        //}
    }
}

