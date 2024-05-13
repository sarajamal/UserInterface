using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models;

namespace Test12.DataAccess.Repository
{
    public class MainsectionRepository : Repository<MainSections>, IMainsectionRepository
    {

        private readonly ApplicationDbContext _context;
        public MainsectionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(MainSections obj)
        {

        }

    }
}
