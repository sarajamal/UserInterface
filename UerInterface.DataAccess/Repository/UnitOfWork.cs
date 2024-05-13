using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.DataAccess.RepositoryPro;
using Test12.DataAccess.RepositoryPro.IRepositoryPro1;

namespace Test12.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IPreparationRepository PreparationRepository { get; private set; } // constructor 
        public IComponentRepository ComponentRepository { get; private set; }
        public IPrepaToolsVarietyRepository PrepaToolsVarietyRepository { get; private set; }
        public IStepsPreparationRepository StepsPreparationRepository { get; private set; }

        public IItemsREpository itemsRepository { get; private set; } // constructor 
        public IComponentRepository2 ComponentRepository2 { get; private set; }
        public IPrepaToolsVarietyRepository2 PrepaToolsVarietyRepository2 { get; private set; }
        public IStepsProductionRepository2 StepsPreparationRepository2 { get; private set; }

        public ITredMarketRepository TredMarketRepository { get; private set; }
        public ILoginRepository loginRepository { get; private set; }

        public ICleanRepository CleanRepository { get; private set; }
        public IStepsCleanRepository3 StepsCleanRepository3 { get; private set; }
        public IDevice_ToolsRepository Device_tools1 { get; private set; }
        public IMainsectionRepository MainsectionRepository { get; private set; }
        public IFoodRepository FoodRepository { get; private set; }
        public IReadyFoodRepository readyFoodRepository { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            PreparationRepository = new PreparationRepository(_context);
            ComponentRepository = new ComponentRepository(_context);
            PrepaToolsVarietyRepository = new PrepaToolsVarietyRepository(_context);
            StepsPreparationRepository = new StepsPreparationRepository(_context);
            itemsRepository = new ItemsRepository(_context);
            ComponentRepository2 = new ComponentRepository2(_context);
            PrepaToolsVarietyRepository2 = new PrepaToolsVarietyRepository2(_context);
            StepsPreparationRepository2 = new StepsProductionRepository2(_context);
            TredMarketRepository = new TredMarketRepository(_context);
            loginRepository = new LoginReposiroty(_context);
            CleanRepository = new CleanRepository(_context);
            StepsCleanRepository3 = new StepsCleanRepository3(_context);
            Device_tools1 = new Device_ToolsRepository(_context);
            MainsectionRepository = new MainsectionRepository(_context);
            FoodRepository = new FoodRepository(_context);
            readyFoodRepository = new ReadyFoodRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }

}
