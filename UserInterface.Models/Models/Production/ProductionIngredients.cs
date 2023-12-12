using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Preparation;

namespace Test12.Models.Models.Production
{
    public class ProductionIngredients
    {
        [Key]
        public int ProdIngredientsID { get; set; }

        [MaxLength(255)]
        public string? ProdIngredientsName { get; set; }
        [MaxLength(255)]
        public string? ProdQuantity { get; set; }
        [MaxLength(255)]
        public string? ProdUnit { get; set; }

        public int ProductionFK { get; set; }
        [ForeignKey("ProductionFK")]
        [ValidateNever]
        public Production? Production { get; set; }


    }
}
