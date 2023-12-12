using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.DataAccess.Repository;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.Production;
using Test12.DataAccess.RepositoryPro.IRepositoryPro1;

namespace Test12.DataAccess.RepositoryPro
{
    public class ComponentRepository2 : Repository<ProductionIngredients>, IComponentRepository2
    {
        private readonly ApplicationDbContext _context;
        public ComponentRepository2(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ProductionIngredients obj)
        {
            var objFormDb = _context.ProductionIngredients.FirstOrDefault(u => u.ProdIngredientsID == obj.ProdIngredientsID);
            if (objFormDb != null)
            {

                objFormDb.ProdQuantity = obj.ProdQuantity;
                objFormDb.ProdUnit = obj.ProdUnit;
                objFormDb.ProdIngredientsName = obj.ProdIngredientsName;

                _context.SaveChanges();
            }
            //_context.Update(obj);

        }

   
    }
}
