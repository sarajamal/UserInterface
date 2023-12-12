using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.Models.Food
{
    public class FoodStuffs
    {
        [Key]
        public int FoodStuffsID { get; set; }  
        public int FoodStuffsNum { get; set; }

        public double? FoodStuffsOrder { get; set; }

        [MaxLength(255)]
        public string? FoodStuffsName { get; set; }
        [MaxLength(255)]
        public string? FoodStuffsImage { get; set; }
        public int? BrandFK { get; set; } = 0; 
        [ForeignKey("BrandFK")]
        [ValidateNever]
        public Brands? Brand { get; set; }


    }
}
