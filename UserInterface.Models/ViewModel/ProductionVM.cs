﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;
using Test12.Models.Models.Production;
using Test12.Models.Models.trade_mark;
using Test12.Models.Models.Preparation;

namespace Test12.Models.ViewModel
{
    public class ProductionVM
    {
        [ValidateNever]
        public Production Productionvm { get; set; } 
        [ValidateNever]
        public Preparations PreparationVM { get; set; }

        [ValidateNever]
        public List<ProductionIngredients> componontVMList2 { get; set; }
        [ValidateNever]
        public List<ProductionTools> ToolsVarityVM2 { get; set; }
        [ValidateNever]
        public List<ProductionSteps> stepsVM2 { get; set; }
        //[ValidateNever]
        //public IEnumerable<SelectListItem> PreparationName { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public IEnumerable<Production> itemsList { get; set; }

        [ValidateNever]
        public Brands tredMaeketVM { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public IEnumerable<Production> itemList33333 { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public LoginTredMarktViewModel welcomTredmarketProduction { get; set; } 

    }
}
