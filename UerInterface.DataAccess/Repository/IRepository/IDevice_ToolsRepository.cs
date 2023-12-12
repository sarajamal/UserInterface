using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Preparation;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IDevice_ToolsRepository : IRepository<DevicesAndTools>
    {
        void Update(DevicesAndTools obj);

    }

}
