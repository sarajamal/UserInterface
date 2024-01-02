using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.ViewModel
{
    public class PreComViewModel
    {
        [ValidateNever]
        public Preparations PreparationVM { get; set; }
        [ValidateNever]
        public List<PreparationIngredients> componontVMList { get; set; }
        [ValidateNever]
        public List<PreparationTools> ToolsVarityVM { get; set; }
        [ValidateNever]
        public List<PreparationSteps> stepsVM { get; set; }

        [ValidateNever]
        public Brands tredMaeketVM { get; set; }
        //[ValidateNever]
        //public IEnumerable<SelectListItem> PreparationName { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public IEnumerable<Preparations> PreparationList { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public IEnumerable<Brands> tredMaeketVMList { get; set; }
        [ValidateNever]
        public LoginTredMarktViewModel WelcomTredMarketPrecomponent { set; get; }
    }
}
