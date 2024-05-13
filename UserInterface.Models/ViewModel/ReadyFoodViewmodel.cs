using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;
using Test12.Models.Models.ReadyFood;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.ViewModel
{
    public class ReadyFoodViewmodel
    {
        [ValidateNever]
        public ReadyProducts ReadyfoodVM { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public List<ReadyProducts> readyfoodlistVM { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public IEnumerable<ReadyProducts> FoodReadyVMorder { get; set; }

        [ValidateNever]
        public Brands tredMaeketReadyfoodVM { get; set; }
        [ValidateNever]
        public LoginTredMarktViewModel WelcomTredmarketReadyFood { get; set; }
    }
}
