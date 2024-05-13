
using Microsoft.EntityFrameworkCore;
using Test12.Models.Models;
using Test12.Models.Models.Clean;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Food;
using Test12.Models.Models.Login;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.Production;
using Test12.Models.Models.ReadyFood;
using Test12.Models.Models.trade_mark;

namespace Test12.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Preparations> Preparations { get; set; }
        public DbSet<Production> Production { get; set; }
        public DbSet<PreparationIngredients> PreparationIngredients { get; set; }
        public DbSet<ProductionIngredients> ProductionIngredients { get; set; }
        public DbSet<PreparationTools> PreparationTools { get; set; }
        public DbSet<ProductionTools> ProductionTools { get; set; }
        public DbSet<PreparationSteps> PreparationSteps { get; set; }
        public DbSet<ProductionSteps> ProductionSteps { get; set; }
        public DbSet<CleaningSteps> CleaningSteps { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<ClientLogin> ClientLogin { get; set; }
        public DbSet<Cleaning> Cleaning { get; set; }
        public DbSet<DevicesAndTools> DevicesAndTools { get; set; }
        public DbSet<MainSections> MainSections { get; set; }
        public DbSet<FoodStuffs> FoodStuffs { get; set; }
        public DbSet<ReadyProducts> ReadyProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);// for IdentityDbContext

            modelBuilder.Entity<Preparations>().ToTable("Preparations");
            modelBuilder.Entity<Production>().ToTable("Production");
            modelBuilder.Entity<PreparationIngredients>().ToTable("PreparationIngredients");
            modelBuilder.Entity<ProductionIngredients>().ToTable("ProductionIngredients");
            modelBuilder.Entity<PreparationTools>().ToTable("PreparationTools");
            modelBuilder.Entity<ProductionTools>().ToTable("ProductionTools");
            modelBuilder.Entity<PreparationSteps>().ToTable("PreparationSteps");
            modelBuilder.Entity<ProductionSteps>().ToTable("ProductionSteps");
            modelBuilder.Entity<CleaningSteps>().ToTable("CleaningSteps");
            modelBuilder.Entity<Brands>().ToTable("Brands");
            modelBuilder.Entity<ClientLogin>().ToTable("ClientLogin");
            modelBuilder.Entity<Cleaning>().ToTable("Cleaning");
            modelBuilder.Entity<DevicesAndTools>().ToTable("DevicesAndTools");
            modelBuilder.Entity<MainSections>().ToTable("MainSections");
            modelBuilder.Entity<FoodStuffs>().ToTable("FoodStuffs");
            modelBuilder.Entity<ReadyProducts>().ToTable("ReadyProducts");
        }
    }
}
