using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models.Clean;

namespace Test12.DataAccess.Repository
{
    public class StepsCleanRepository3 : Repository<CleaningSteps>, IStepsCleanRepository3
    {
        private readonly ApplicationDbContext _context;
        public StepsCleanRepository3(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(CleaningSteps obj)
        {
            var objFormDb = _context.CleaningSteps.FirstOrDefault(u => u.CleaStepsID == obj.CleaStepsID);
            if (objFormDb != null)
            {
                // Update properties for Step 1
                objFormDb.CleaText = obj.CleaText;

                if (obj.CleaStepsImage != null)
                {
                    objFormDb.CleaStepsImage = obj.CleaStepsImage;
                }
                // Update properties for Step 2


                _context.SaveChanges();

                //_context.Update(obj);

            }

        }
        public int GetLastStepId()
        {
            // If there are no entries in the table, return 0 or an appropriate default value
            if (!_context.CleaningSteps.Any())
            {
                return 0;
            }
            // Retrieve and return the max CleaStepsID
            return _context.CleaningSteps.Max(p => p.CleaStepsID);
        }
    }
}
