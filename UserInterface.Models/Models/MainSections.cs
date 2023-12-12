using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.Models
{
    public class MainSections
    {
        [Key]
        public int MainSectionsID { get; set; }
        public double? MainSectionsOrder { get; set; }
        [MaxLength(255)]
        public string? SectionsName { get; set; }
        public string? SectionsImage { get; set; }

        public int? BrandFK { get; set; }
        [ForeignKey("BrandFK")]
        [ValidateNever]
        public Brands? Brand { get; set; }

    }
}
