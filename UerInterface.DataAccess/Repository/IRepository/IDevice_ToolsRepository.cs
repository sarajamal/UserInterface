using Test12.Models.Models.Device_Tools;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IDevice_ToolsRepository : IRepository<DevicesAndTools>
    {
        void Update(DevicesAndTools obj);
        int GetLastStepId();

    }

}
