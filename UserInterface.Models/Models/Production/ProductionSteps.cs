using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test12.Models.Models.Production
{
    public class ProductionSteps
    {
        [Key]
        public int ProdStepsID { get; set; }

        public string? ProdText { get; set; }
        public int? ProdStepsNum { get; set; }

        [MaxLength(255)]
        public string? ProdSImage { get; set; }

        public int ProductionFK { get; set; }
        [ForeignKey("ProductionFK")]
        [ValidateNever]
        public Production? Production { get; set; }


    }
}
