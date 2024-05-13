using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.Models.ReadyFood
{
    public class ReadyProducts
    {
        [Key]
        public int ReadyProductsID { get; set; }

        public double? ReadyProductsOrder { get; set; }

        [MaxLength(255)]
        public string? ReadyProductsName { get; set; }
        [MaxLength(255)]
        public string? ReadyProductsImage { get; set; }

        public int? BrandFK { get; set; }
        [ForeignKey("BrandFK")]
        [ValidateNever]
        public Brands? Brand { get; set; }
    }
}
