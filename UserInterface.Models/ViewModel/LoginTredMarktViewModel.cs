using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;
using Test12.Models.Models;
using Test12.Models.Models.Clean;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Food;
using Test12.Models.Models.Login;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.Production;
using Test12.Models.Models.ReadyFood;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.ViewModel
{
    public class LoginTredMarktViewModel
    {
        [ValidateNever]
        [JsonIgnore]
        public List<Brands> tredList { get; set; }

        [ValidateNever]
        public Brands TredMarktVM { get; set; } 
        [ValidateNever]
        public ClientLogin LoginVM { get; set; }
        [ValidateNever]
        public List<Preparations> PreparatonLoginVMlist { get; set; }

        [ValidateNever]
        public List<Production> ProductionLoginVMlist { get; set; }

        [ValidateNever]
        public List<FoodStuffs> FoodLoginVMlist { get; set; }

        [ValidateNever]
        public List<ReadyProducts> ReadyFoodLoginVMlist { get; set; }
     
        [ValidateNever]
        public List<Cleaning> CleanLoginVMlist { get; set; }

        [ValidateNever]
        public List <MainSections> MainsectionVMlist { get; set; }

        [ValidateNever]
        public MainSections MainsectionVM { get; set; }

        [ValidateNever]
        public Preparations PreparatonLoginVM { get; set; }

        [ValidateNever]
        public Production ProductionLoginVM { get; set; }

        [ValidateNever]
        public DevicesAndTools DeviceToolsLoginVM { get; set; }

        [ValidateNever]
        public Cleaning CleanLoginVM { get; set; }

        [ValidateNever]
        public FoodStuffs FoodLoginVM { get; set; }

        [ValidateNever]
        public ReadyProducts ReadyFoodLoginVM { get; set; }
  
    }
}
