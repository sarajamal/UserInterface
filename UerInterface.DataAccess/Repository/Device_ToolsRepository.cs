using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.DataAccess.RepositoryPro.IRepositoryPro1;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Production;

namespace Test12.DataAccess.Repository
{
    public class Device_ToolsRepository : Repository<DevicesAndTools>, IDevice_ToolsRepository
    {
        private readonly ApplicationDbContext _context;
        public Device_ToolsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(DevicesAndTools obj)
        {

            _context.SaveChanges();

        }
    }
}
