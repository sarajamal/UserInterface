using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.Models.Clean
{
    public class Cleaning
    {

        [Key]
        public int CleaningID { get; set; }
        public double? CleaningOrder { get; set; }
        [ValidateNever]
        [MaxLength(255)]
        public string? DeviceName { get; set; }
        [MaxLength(255)]
        [ValidateNever]
        public string? Note { get; set; }

        public int? BrandFK { get; set; }
        [ForeignKey("BrandFK")]
        [ValidateNever]
        public Brands? Brand { get; set; }
    }
}
