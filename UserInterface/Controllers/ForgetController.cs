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
using System.Net.Http;


namespace Test12.Controllers
{
    public class ForgetController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        public ForgetController(IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
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
                LoginVM = new ClientLogin(),

            };

            LogVM.LoginVM = _unitOfWork.loginRepository.Get(u => u.Email == email);

            TempData["Email"] = email;
            return View(LogVM);
        }

        [HttpPost]
        public async Task<IActionResult> setForgetPassword(LoginTredMarktViewModel model)
        {
            var user = _unitOfWork.loginRepository.Get(u => u.Email == model.LoginVM.Email);

            if (user != null)
            {
                var formData = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("password", model.LoginVM.Password),
            new KeyValuePair<string, string>("cost", "10") // Specify the cost factor
        });

                var request = new HttpRequestMessage(HttpMethod.Post, "https://www.toptal.com/developers/bcrypt/api/generate-hash.json")
                {
                    Content = formData
                };

                HttpResponseMessage response;
                try
                {
                    response = await _httpClient.SendAsync(request);
                }
                catch (HttpRequestException ex)
                {
                    // Handle exceptions (log them, show error message, etc.)
                    ModelState.AddModelError(string.Empty, "خطأ أثناء معالجة طلبك ");
                    return View(model);
                }

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);

                    if (result != null && result.hash != null)
                    {
                        string hashedPassword = result.hash;

                        // Update the user's password with the new hash
                        user.Password = hashedPassword;

                        // Save the changes to the database
                        _unitOfWork.loginRepository.Update(user);
                        _unitOfWork.Save();

                        // Redirect to a success page or perform other actions
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Handle cases where the API call was not successful
                ModelState.AddModelError(string.Empty, "خطأ في إعادة تعيين كلمة المرور ");
                return View(model);
            }

            // Handle the case where the user is not found
            ModelState.AddModelError(string.Empty, "المستخدم غير موجود");
            return View(model);
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
                else if (email != null)
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
            mail.From = new MailAddress("bdooncode5@gmail.com");  // Set the sender's email address

            mail.To.Add(new MailAddress(email));
            mail.Subject = "إعادة تعيين كلمة المرور";

            string resetUrl = Url.Action("setForgetPassword", "Forget", new { token = token, email = email }, protocol: HttpContext.Request.Scheme);

            // Set the body of the email
            string body = $@"
            <html>
            <head>
                <style>
                    .email-container {{ font-family: Arial, sans-serif; max-width: 600px; margin: auto; }}
                    .email-content {{ text-align: center; }}
                    .email-button {{ background-color: #004aad; color: #fff; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block; }}
                    .ii a[href] {{
                        color:#fff; }}
                </style>
            </head>
            <body>
                <div class='email-container'>
                    <div class='email-content'>
                        <h2>إعادة تعيين كلمة المرور</h2>
                        <p>لقد طلبت إعادة تعيين كلمة المرور لحسابك. يرجى النقر على الرابط أدناه لتعيين كلمة مرور جديدة:</p>
                        <a class='email-button' href='{resetUrl}'>إعادة تعيين كلمة المرور</a>
                    </div>
                </div>
            </body>
            </html>";

            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("bdooncode5@gmail.com", "nzzc qhtn rgoc fthv");
            smtp.EnableSsl = true;

            // Send the email
            smtp.Send(mail);
        }


    }
}
