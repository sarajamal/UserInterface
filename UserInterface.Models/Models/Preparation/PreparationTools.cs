
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test12.Models.Models.Preparation
{
    public class PreparationTools
    {
        [Key]
        public int PrepToolsID { get; set; }

        [MaxLength(255)]
        public string? PrepTools { get; set; }

        public int PreparationsFK { get; set; }
        [ForeignKey("PreparationsFK")]
        [ValidateNever]
        public Preparations? Preparation { get; set; }
    }
}
