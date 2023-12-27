using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.trade_mark;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Test12.Models.Models.Clean
{
    public class CleaningSteps
    {
        [Key]
        public int CleaStepsID { get; set; }

        public string? CleaText { get; set; }
        public int? CleaStepsNum { get; set; }
        [ValidateNever]
        [MaxLength(255)]
        public string? CleaStepsImage { get; set; }

        public int CleaningFK { get; set; }
        [ForeignKey("CleaningFK")]
        [ValidateNever]
        public Cleaning? Cleaning { get; set; }
    }
}
