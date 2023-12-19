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

      


    }
}
