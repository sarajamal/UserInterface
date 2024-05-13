using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test12.Models.Models.Preparation
{
    public class PreparationIngredients
    {
        [Key]
        public int PrepIngredientsID { get; set; }

        [MaxLength(255)]
        public string? PrepIngredientsName { get; set; }
        [MaxLength(255)]
        public string? PrepQuantity { get; set; }
        [MaxLength(255)]
        public string? PrepUnit { get; set; }

        public int PreparationsFK { get; set; }
        [ForeignKey("PreparationsFK")]
        [ValidateNever]
        public Preparations? Preparation { get; set; }


    }
}
