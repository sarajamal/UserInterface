
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test12.Models.Models.trade_mark;

namespace Test12.Models.Models.Login
{
    public class UsersT
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }

        [MaxLength(255)]
        public string ClientName { get; set; }

        [MaxLength(255)]
        [Required, EmailAddress, Display(Name = "أدخل البريد الإلكتروني")]
        public string Email { get; set; }

        [MaxLength(255)]
        public string BrandName { get; set; }
        [MaxLength(255)]
        public string Nots { get; set; }

        public DateTime ExpirationDate { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsManager { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsClient { get; set; }
        public bool IsUser { get; set; }
        public bool AllowAddition { get; set; }
        public bool AllowEdits { get; set; }
        public bool AllowDeletion { get; set; }

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
