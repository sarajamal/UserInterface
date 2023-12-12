using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Food;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.ViewModel
{
    public class FoodVM
    {
        [ValidateNever]
        public FoodStuffs FoodViewM { get; set; } = new FoodStuffs();
        [ValidateNever]
        [JsonIgnore]
        public List<FoodStuffs> FoodViewMList { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public IEnumerable<FoodStuffs> FoodsVMorder { get; set; }
        [ValidateNever]
        public Brands tredMaeketFoodsVM { get; set; }
    }
}
