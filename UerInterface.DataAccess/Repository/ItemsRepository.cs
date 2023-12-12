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
    public class ItemsRepository : Repository<Production>, IItemsREpository //هذي الطريقة حتى ما يجيب كل Methods الموجودة في Generic
    {
        private readonly ApplicationDbContext _context;
        public ItemsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Production obj)
        {


            var objFormDb = _context.Production.FirstOrDefault(u => u.ProductionID == obj.ProductionID);
            if (objFormDb != null)
            {
                objFormDb.ProductName = obj.ProductName;
          
                // Retrieve the current value of رقم_النسخة from the database
                string VersionNumber = objFormDb.VersionNumber;

                // Parse it to an integer
                int numericPart;

                //.TryParse used for (converting) a string representation of an integer into an actual integer value.
                if (int.TryParse(VersionNumber, out numericPart))
                {
                    // Increment the integer
                    numericPart++;

                    // Update رقم_النسخة with the new incremented value
                    objFormDb.VersionNumber = numericPart.ToString();
                }

                objFormDb.ProductType = obj.ProductType;
                objFormDb.Expiry = obj.Expiry;
                objFormDb.Station = obj.Station;
                objFormDb.PreparationTime = obj.PreparationTime;
                objFormDb.component2 = obj.component2;

                if (obj.ProductImage != null)
                {
                    objFormDb.ProductImage = obj.ProductImage;
                }
            }


        }
    }

}
