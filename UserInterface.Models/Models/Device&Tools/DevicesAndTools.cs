using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.Models.Device_Tools
{
    public class DevicesAndTools
    {
        [Key]
        public int DevicesAndToolsID { get; set; } 
        public int DevicesAndTools_Num { get; set; }

        public double? DevicesAndToolsOrder { get; set; }

        [MaxLength(255)]
        public string? DevicesAndTools_Name { get; set; }
        [MaxLength(255)]
        [ValidateNever]
        public string? DevicesAndTools_Image { get; set; }

        public int? BrandFK { get; set; }
        [ForeignKey("BrandFK")]
        [ValidateNever]
        public Brands? Brand { get; set; }


    }
}
