using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository;
using Test12.DataAccess.Repository.IRepository;
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
            var objFormDb = _context.ProductionSteps.FirstOrDefault(u => u.ProductionFK == obj.ProductionFK);
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

        //public void Delete (الخطوات obj)
        //{
        //   var objFormDb = _context.الخطوات.FirstOrDefault(u => u.ID == obj.ID); 
        //    if(objFormDb != null)
        //    {

        //    }
        //}
    }
    
}
