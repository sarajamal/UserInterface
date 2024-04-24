using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Login;
using Test12.Models.Models.Production;

namespace Test12.Models.Models.trade_mark
{
    public class Brands
    {
        [Key]
        public int BrandID { get; set; } = 0;

        [MaxLength(255)]
        public string? BrandName { get; set; }
        [ValidateNever]
        public string? BrandCoverImage { get; set; }
        public DateTime? Date { get; set; }

        [ValidateNever]
        public string? BrandLogoImage { get; set; }
        
        [ValidateNever]
        [MaxLength(255)]
        public string? CreatedBY { get; set; }   
        [ValidateNever]
        [MaxLength(255)]
        public string? ClientName { get; set; }
        [ValidateNever]
        public string? BrandFooterImage { get; set; }
        

    }
}
