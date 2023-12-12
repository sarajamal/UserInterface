using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace Test12.Models.Models.Production
{
    public class ProductionTools
    {
        [Key]
        public int ProdToolsID { get; set; }

        [MaxLength(255)]
        public string? ProdTools { get; set; }

        public int ProductionFK { get; set; }
        [ForeignKey("ProductionFK")]
        [ValidateNever]
        public Production? Production { get; set; }
    }
}
