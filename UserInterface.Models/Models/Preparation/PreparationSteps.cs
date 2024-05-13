using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test12.Models.Models.Preparation
{
    public class PreparationSteps
    {
        [Key]
        public int PrepStepsID { get; set; }

        public string? PrepText { get; set; }
        public int? PrepStepsNum { get; set; }

        [MaxLength(255)]
        public string? PrepImage { get; set; }

        public int PreparationsFK { get; set; }
        [ForeignKey("PreparationsFK")]
        [ValidateNever]
        public Preparations? Preparation { get; set; }


    }
}
