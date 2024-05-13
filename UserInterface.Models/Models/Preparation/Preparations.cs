
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.Models.Preparation
{
    public class Preparations
    {

        [Key]
        public int PreparationsID { get; set; }
        public double? PreparationsOrder { get; set; }

        [MaxLength(255)]
        public string? prepareName { get; set; }

        [MaxLength(255)]
        public string? VersionNumber { get; set; }
        [MaxLength(255)]

        public string? NetWeight { get; set; }
        [MaxLength(255)]

        public string? Expiry { get; set; }
        [MaxLength(255)]

        public string? Station { get; set; }
        [MaxLength(255)]

        public string? PreparationTime { get; set; }
        [ValidateNever]
        public string? prepareImage { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public ICollection<PreparationIngredients> component { get; set; }

        public int? BrandFK { get; set; }
        [ForeignKey("BrandFK")]
        [ValidateNever]
        public Brands? Brand { get; set; }

    }
}
