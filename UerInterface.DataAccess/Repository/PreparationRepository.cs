using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository
{
    public class PreparationRepository : Repository<Preparations>, IPreparationRepository //هذي الطريقة حتى ما يجيب كل Methods الموجودة في Generic
    {
        private readonly ApplicationDbContext _context;
        public PreparationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Preparations obj)
        {


            var objFormDb = _context.Preparations.FirstOrDefault(u => u.PreparationsID == obj.PreparationsID);
            if (objFormDb != null)
            {
                objFormDb.prepareName = obj.prepareName;

                // Retrieve the current value of رقم_النسخة from the database
                string رقم_النسخة = objFormDb.VersionNumber;

                // Parse it to an integer
                int numericPart;

                //.TryParse used for (converting) a string representation of an integer into an actual integer value.
                if (int.TryParse(رقم_النسخة, out numericPart))
                {
                    // Increment the integer
                    numericPart++;

                    // Update رقم_النسخة with the new incremented value
                    objFormDb.VersionNumber = numericPart.ToString();
                }

                objFormDb.NetWeight = obj.NetWeight;
                objFormDb.Expiry = obj.Expiry;
                objFormDb.Station = obj.Station;
                objFormDb.PreparationTime = obj.PreparationTime;
                //objFormDb.component = obj.component;

                if (obj.prepareImage != null)
                {
                    objFormDb.prepareImage = obj.prepareImage;
                }
            }


        }
    }

}

