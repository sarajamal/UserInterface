using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Test12.Models.Models.Clean;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.ViewModel
{
    public class CleanVM
    {
        [ValidateNever]
        public Cleaning CleanViewModel { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public List<Cleaning> CleanList { get; set; }

        public List<CleaningSteps> CleaningSteps { get; set; }

        [ValidateNever]
        public Brands tredMaeketCleanVM { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public IEnumerable<Cleaning> CleaningVMorder { get; set; }
    }
}
