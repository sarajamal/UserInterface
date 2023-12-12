using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.Models.Production
{
    public class Production
    {

        [Key]
        public int ProductionID { get; set; }
        
        public double? ProductionOrder { get; set; }

        public string? ProductName { get; set; }

        [MaxLength(255)]
        public string? VersionNumber { get; set; }
       
        [BindProperty]
        public string? ProductType { get; set; }
      
        public string? Expiry { get; set; }
 
        public string? Station { get; set; }
 
        public string? PreparationTime { get; set; }
        [ValidateNever]
        public string? ProductImage { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public ICollection<ProductionIngredients> component2 { get; set; }

        public int? BrandFK { get; set; }
        [ForeignKey("BrandFK")]
        [ValidateNever]
        public Brands? Brands { get; set; }

    }
}
