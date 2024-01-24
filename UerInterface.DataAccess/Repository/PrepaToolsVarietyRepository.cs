using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository
{
    public class PrepaToolsVarietyRepository : Repository<PreparationTools>,IPrepaToolsVarietyRepository
    {
        private readonly ApplicationDbContext _context;
        public PrepaToolsVarietyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(PreparationTools obj)
        {

            _context.Update(obj);

        }

        public int GetLastToolsId()
        {
            // If there are no entries in the table, return 0 or an appropriate default value
            if (!_context.PreparationTools.Any())
            {
                return 0;
            }

            // Retrieve and return the max PrepStepsID
            return _context.PreparationTools.Max(p => p.PrepToolsID);
        }


    }
}
