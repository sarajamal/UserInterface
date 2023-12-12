using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.trade_mark;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Test12.Models.Models.Clean
{
    public class الخطوات3
    {
        [Key]
        public int ID { get; set; }

        public string? الخطوة1 { get; set; }
        public int? رقم_الخطوة1 { get; set; }

        [MaxLength(255)]
        public string? الصورة1 { get; set; }

        public string? الخطوة2 { get; set; }
        public int? رقم_الخطوة2 { get; set; }
        [MaxLength(255)]
        public string? الصورة2 { get; set; }

        public int ID_Tandeef1 { get; set; }
        [ForeignKey("ID_Tandeef1")]
        [ValidateNever]
        public Cleaning? التنظيف { get; set; }
    }
}
