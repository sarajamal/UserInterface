using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository
{
    public class ComponentRepository : Repository<PreparationIngredients>, IComponentRepository
    {
        private readonly ApplicationDbContext _context;
        public ComponentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(PreparationIngredients obj)
        {
            var objFormDb = _context.PreparationIngredients.FirstOrDefault(u => u.PrepIngredientsID == obj.PrepIngredientsID);
            if (objFormDb != null)
            {

                objFormDb.PrepIngredientsName = obj.PrepIngredientsName;
                objFormDb.PrepUnit = obj.PrepUnit;
                objFormDb.PrepQuantity = obj.PrepQuantity;

                _context.SaveChanges();
            }
            //_context.Update(obj);

        }
        public int GetLastComponentId()
        {
            // If there are no entries in the table, return 0 or an appropriate default value
            if (!_context.PreparationIngredients.Any())
            {
                return 0;
            }

            // Retrieve and return the max PrepStepsID
            return _context.PreparationIngredients.Max(p => p.PrepIngredientsID);
        }


    }
}

