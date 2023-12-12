using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.Models.Login
{
    public class LoginModels
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Login_ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [MaxLength(255)]
        [Required, EmailAddress, Display(Name = "أدخل البريد الإلكتروني")]
        public string Email { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int BrandFK { get; set; }
        [ForeignKey("BrandFK")]
        [ValidateNever]
        public Brands? Brand { get; set; }
    }

    //[Display(Name = "Remember me?")]
    //public bool RememberMe { get; set; }

    //public InputModel Input { get; set; }

    //public IList<AuthenticationScheme> ExternalLogins { get; set; }

    //public string ReturnUrl { get; set; }

    //[TempData]
    //public string ErrorMessage { get; set; }

    //public class InputModel
    //{
    //    [Required]
    //    [MaxLength(255)]
    //    public string Username { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }

    //    [Display(Name = "تذكرني ؟")]
    //    public bool RememberMe { get; set; }
    //}
}
