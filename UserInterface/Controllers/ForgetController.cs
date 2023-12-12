using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Test12.Models.ViewModel;
using Test12.DataAccess.Repository.IRepository;
using NuGet.Common;
using Test12.Models.Models.Food;
using Test12.Models.Models.trade_mark;
using Test12.Models.Models.Login;


namespace Test12.Controllers
{
    public class ForgetController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ForgetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //الكود لنسيت كلمة المرور
        private const int TokenExpirationMinutes = 30; // Adjust the expiration time as needed

        public IActionResult ForgetPassword()
        {

            return View();
        }

        //صفحة كلمة المرور الجديدة 
        [HttpGet]
        public IActionResult setForgetPassword(string email)
        {
            LoginTredMarktViewModel LogVM = new()
            {
                LoginVM = new LoginModels(),
               
            };

            LogVM.LoginVM = _unitOfWork.loginRepository.Get(u => u.Email == email);

            TempData["Email"] = email;                               
            return View(LogVM);
        }
        [HttpPost]
        public IActionResult setForgetPassword(LoginTredMarktViewModel model)
        {
            // Assuming GetByEmail is a method in your loginRepository to get a user by email
            var user = _unitOfWork.loginRepository.Get(u=> u.Email == model.LoginVM.Email);

            if (user != null)
            {
                // Hash the new password before updating
                string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(model.LoginVM.Password, workFactor: 13);

                // Update the hashed password
                user.Password = hashedPassword;

                // Save the changes to the database
                _unitOfWork.loginRepository.Update(user);
                _unitOfWork.Save();

                // Redirect to a success page or perform other actions
                return RedirectToAction("Index", "Home");
            }

            // Handle the case where the user with the specified email was not found
            return RedirectToAction("PasswordResetFailure");
        }

        [HttpPost]
        public IActionResult ForgetPassword(LoginTredMarktViewModel model)
        {
            string message = "";
            bool statuse = false; 
            if (ModelState.IsValid)
            {
                var email = _unitOfWork.loginRepository.Get(u => u.Email == model.LoginVM.Email);

                //code her
                if (string.IsNullOrEmpty(model.LoginVM.Email))
                {
                    ModelState.AddModelError(string.Empty, "أدخل البريد الإلكتروني *");

                }
               else if( email != null)
                {
                    (string token, DateTime expirationTime) = GenerateSecureTokenWithExpiration();
                  
                    SendResetEmail(email.Email, token);
                   

                    ModelState.AddModelError(string.Empty, "تم إرسال رابط إعادة تعيين كلمة المرور على بريدك الالكتروني . ");
                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "البريد غير مسجل لدينا .");  
                }
            }
            return View(model);
        }

        private (string token, DateTime expirationTime) GenerateSecureTokenWithExpiration()
        {
            // Generate a secure random token using RNGCryptoServiceProvider
            const int tokenLength = 32; // You can adjust the length based on your needs
            byte[] data = new byte[tokenLength];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(data);
            }

            string token = Convert.ToBase64String(data);

            // Set the expiration time for the token
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(TokenExpirationMinutes);

            return (token, expirationTime);
        }

        private void SendResetEmail(string email, string token)
        {     
            //create mail message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("sarajk12248@gmail.com");  // Set the sender's email address

            mail.To.Add(new MailAddress(email));
            mail.Subject = "إعادة تعيين كلمة المرور";

            string resetUrl = Url.Action("setForgetPassword", "Forget", new { token = token, email = email }, protocol: HttpContext.Request.Scheme);

            // Set the body of the email
            string body = "<div class=\"card-body p-4\">" +
                          "<div class=\"row\">" +
                          "<div class=\"col-md-12\">" +
                          "<section>" +
                          "<p style=\"text-align:center;font:bold\">إعادة تعيين كلمة المرور</p>" +
                          "<div class=\"form-group\">" +
                          $"لقد طلبت تعيين كلمة المرور لحسابك بعنوان البريد الإلكتروني: انقر على الرابط أدناه لإعادة تعيين كلمة المرور الخاصة بك:" +
                          $"<a href='{resetUrl}'>إعادة تعيين كلمة المرور</a>" +
                          "</div>" +
                          "<div>" +
                          "</div>" +
                          "</section>" +
                          "</div>" +
                          "</div>" +
                          "</div>";

            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("sarajk12248@gmail.com", "aeds mqca qklv layc");
            smtp.EnableSsl = true;
     
            // Send the email
            smtp.Send(mail);
        }


    }
}
