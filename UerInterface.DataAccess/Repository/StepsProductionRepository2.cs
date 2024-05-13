using Microsoft.EntityFrameworkCore;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository;
using Test12.DataAccess.RepositoryPro.IRepositoryPro1;
using Test12.Models.Models.Production;

namespace Test12.DataAccess.RepositoryPro
{
    public class StepsProductionRepository2 : Repository<ProductionSteps>, IStepsProductionRepository2
    {
        private readonly ApplicationDbContext _context;
        public StepsProductionRepository2(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ProductionSteps obj)
        {
            var objFormDb = _context.ProductionSteps.FirstOrDefault(u => u.ProdStepsID == obj.ProdStepsID);
            if (objFormDb != null)
            {
                // Update properties for Step 1
                objFormDb.ProdText = obj.ProdText;

                if (obj.ProdSImage != null)
                {
                    objFormDb.ProdSImage = obj.ProdSImage;
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
            if (!_context.ProductionSteps.Any())
            {
                return 0;
            }

            // Retrieve and return the max PrepStepsID
            return _context.ProductionSteps.Max(p => p.ProdStepsID);
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
