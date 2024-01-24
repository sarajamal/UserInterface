using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository;
using Test12.DataAccess.RepositoryPro.IRepositoryPro1;
using Test12.Models.Models.Production;

namespace Test12.DataAccess.RepositoryPro
{
    public class PrepaToolsVarietyRepository2 : Repository<ProductionTools>, IPrepaToolsVarietyRepository2
    {
        private readonly ApplicationDbContext _context;
        public PrepaToolsVarietyRepository2(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ProductionTools obj)
        {

            _context.Update(obj);

        }

        public int GetLastToolsId()
        {
            // If there are no entries in the table, return 0 or an appropriate default value
            if (!_context.ProductionTools.Any())
            {
                return 0;
            }

            // Retrieve and return the max PrepStepsID
            return _context.ProductionTools.Max(p => p.ProdToolsID);
        }


    }
    
}
