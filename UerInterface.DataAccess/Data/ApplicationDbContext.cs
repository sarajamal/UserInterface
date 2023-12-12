
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Test12.Models.Models;
using Test12.Models.Models.Clean;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.Production;
using Test12.Models.Models.trade_mark;
using Test12.Models.Models.Food;
using Test12.Models.Models.ReadyFood;
using Test12.Models.Models.Login;

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
        public DbSet<الخطوات3> الخطوات3 { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<LoginModels> LoginModels { get; set; }
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
            modelBuilder.Entity<الخطوات3>().ToTable("الخطوات3");
            modelBuilder.Entity<Brands>().ToTable("Brands");
            modelBuilder.Entity<LoginModels>().ToTable("LoginModels");
            modelBuilder.Entity<Cleaning>().ToTable("Cleaning");
            modelBuilder.Entity<DevicesAndTools>().ToTable("DevicesAndTools");
            modelBuilder.Entity<MainSections>().ToTable("MainSections");
            modelBuilder.Entity<FoodStuffs>().ToTable("FoodStuffs");
            modelBuilder.Entity<ReadyProducts>().ToTable("ReadyProducts");
        }
    }
}
