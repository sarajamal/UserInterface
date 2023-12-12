using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.RepositoryPro.IRepositoryPro1;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPreparationRepository PreparationRepository { get; }
        IComponentRepository ComponentRepository { get; }
        IPrepaToolsVarietyRepository PrepaToolsVarietyRepository { get; }
        IStepsPreparationRepository StepsPreparationRepository { get; }
        IItemsREpository itemsRepository { get; }
        IComponentRepository2 ComponentRepository2 { get; }
        IPrepaToolsVarietyRepository2 PrepaToolsVarietyRepository2 { get; }
        IStepsProductionRepository2 StepsPreparationRepository2 { get; }
        ITredMarketRepository TredMarketRepository { get; }
        ILoginRepository loginRepository { get; }
       ICleanRepository CleanRepository { get; }
        IStepsCleanRepository3 StepsCleanRepository3 { get; }
        IDevice_ToolsRepository Device_tools1 { get; }
        IMainsectionRepository MainsectionRepository { get; }
        IFoodRepository FoodRepository { get; }
        IReadyFoodRepository readyFoodRepository { get; }
       

        void Save();
    }
}
