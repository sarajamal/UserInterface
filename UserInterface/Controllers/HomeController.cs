using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Test12.DataAccess.Repository.IRepository;
using Test12.Models.Models;
using Test12.Models.ViewModel;
using Test12.Models.Models.Preparation;
using Test12.Models.Models.trade_mark;
using System.Text;
using Test12.Models.Models.Clean;
using Test12.Models.Models.Device_Tools;
using Test12.Models.Models.Food;
using Test12.Models.Models.Login;
using Test12.Models.Models.ReadyFood;
using Test12.Models.Models.Production;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Test12.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


      
        //[HttpPost]
        //public async Task<IActionResult> ForgetPassword(LoginModels loginVM)
        //{

        //    // Retrieve user information based on loginVM.Email
        //    var user = await GetUserByEmail(loginVM.Email);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "الايميل خاطئ"); // Incorrect username or password
        //        return View();
        //    }

        //    // Generate a password reset token
        //    var token = Guid.NewGuid().ToString();

        //    // Send password reset email to loginVM.Email
        //    var emailBody = GeneratePasswordResetEmailBody( token);
        //    await SendEmail(user.Email, "إعادة تعيين كلمة المرور", emailBody);


        //    // Store password reset token in data store
        //    await StorePasswordResetToken(user.ID, token);

        //    // Redirect to password reset confirmation page
        //    return RedirectToAction("PasswordResetConfirmation");
        //}
        //private string GeneratePasswordResetEmailBody( string token)
        //{
        //    var baseUrl = Request.Scheme + "://" + Request.Host + "/Home/ForgetPassword";
        //    var resetPasswordUrl = baseUrl + "?token=" + token;

        //    return $"لقد طلبت إعادة تعيين كلمة المرور لحسابك المرتبط بعنوان البريد الإلكتروني " +
        //        $"لإعادة تعيين كلمة المرور الخاصة بك، الرجاء الضغط على الرابط التالي:" +
        //        $" {resetPasswordUrl}";
        //}

        ////للتأكد من أن الايميل موجود في قاعدة البيانات او لا 
        //private async Task<LoginModels> GetUserByEmail(string email)
        //{
        //    var user = _unitOfWork.loginRepository.Get(u => u.Email == email);

        //    // Check if the user exists
        //    if (user != null)
        //    {
        //        return user; // Return the existing user
        //    }

        //    return null; // User with the specified email not found
        //}
        //private async Task SendEmail(string emailAddress, string subject, string body)
        //{
        //    var smtpClient = new SmtpClient("your-smtp-server.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential("your-email@example.com", "your-email-password"),
        //        EnableSsl = true,
        //    };

        //    var mailMessage = new MailMessage("your-email@example.com", emailAddress)
        //    {
        //        Subject = subject,
        //        Body = body,
        //        IsBodyHtml = true,
        //    };

        //    await smtpClient.SendMailAsync(mailMessage);
        //}
        //private async Task StorePasswordResetToken(int userId, string token)
        //{
        //    // Replace this with your actual code for storing the password reset token in your data store
        //    // using the provided user ID and token
        //    Console.WriteLine($"Storing password reset token for user {userId}: {token}");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginTredMarktViewModel model, string returnUrl = null)
        {
            var user = _unitOfWork.loginRepository.Get(u => u.Username == model.LoginVM.Username);

            if (user != null)
            {
                // Hash the new password before updating
                string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(model.LoginVM.Password, workFactor: 13);

                // Update the hashed password
                user.Password = hashedPassword;

                // Save the changes to the database
                _unitOfWork.loginRepository.Update(user);
                _unitOfWork.Save();

                bool isSucces = await _unitOfWork.loginRepository.VerifyUserCredentials(user.Username, model.LoginVM.Password);

                if (isSucces)
                {
                    // Record the user's last activity time in the session
                    RecordUserActivity(user.Login_ID);

                    return RedirectToLocal(returnUrl, user.Login_ID, true); // Successful login
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "خطأ في اسم المستخدم أو كلمة المرور."); // Incorrect username or password
                    return View(model);
                }
            }
            return View(model);
        }

        private void RecordUserActivity(int userId)
        {
            // Store the current date/time as the user's last login time in the database
            _unitOfWork.loginRepository.UpdateLastLoginTime(userId, DateTime.Now);

            // Store the current date/time as the user's last activity in the session
            HttpContext.Session.SetString($"LastActivity_{userId}", DateTime.Now.ToString());
        }

        // Pass the authenticated flag to UserInformation action
        public IActionResult RedirectToLocal(string returnUrl, int id, bool isAuthenticated)
        {
            // Check the last activity time
            if (IsUserInactive(id))
            {
                // Perform logout - clear authentication and session for the user
                HttpContext.SignOutAsync();
                return RedirectToAction(nameof(Index));
            }

            else
            {
                return RedirectToAction("UserInformation", new { id = id, isAuthenticated = isAuthenticated });
            }
        }

        private bool IsUserInactive(int userId)
        {
            // Define a threshold for inactivity duration (e.g., 30 minutes)
            TimeSpan inactivityThreshold = TimeSpan.FromMinutes(60);

            if (HttpContext.Session.TryGetValue($"LastActivity_{userId}", out byte[] lastActivity))
            {
                DateTime lastActivityTime = DateTime.Parse(Encoding.UTF8.GetString(lastActivity));
                TimeSpan inactiveDuration = DateTime.Now - lastActivityTime;
                return inactiveDuration > inactivityThreshold;
            }

            return true; // Treat as inactive if last activity not found
        }

        public IActionResult UserInformation(int? id/*, bool? isAuthenticated*/)
        {
            if (id == null)
            {
                // Handle the case where the id is null, for example:
                return RedirectToAction("Error");
            }

            LoginTredMarktViewModel LoMarket = new()
            {
                LoginVM = new LoginModels(),
                TredMarktVM = new Brands(),
                tredList = new List<Brands>(),
            };
            LoMarket.LoginVM = _unitOfWork.loginRepository.Get(u => u.BrandFK == id);
            LoMarket.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.BrandID == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view
            LoMarket.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            // Populate the model

            //ViewBag.IsAuthenticated = isAuthenticated.HasValue ? isAuthenticated.Value : false;


            return View(LoMarket);
            //return RedirectToAction("UserInformation", "MainsectionView", new { id = nn });
        }

        //[HttpPost]
        //public IActionResult UserInformation(int? id)
        //{
        //    if (id == null)
        //    {
        //        // Handle the case where the id is null, for example:
        //        return RedirectToAction("Error");
        //    }

        //    LoginTredMarktViewModel LoMarket = new()
        //    {

        //        TredMarktVM = new العلامة_التجارية(),
        //        //tredList = new List<العلامة_التجارية>(),
        //        //PreparatonLoginVM = new List<التحضيرات1>(),
        //        MainsectionVM = new List<الأقسام_الرئيسية>()
        //    };
        //    LoMarket.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.ID == id);
        //    LoMarket.MainsectionVM = _unitOfWork.MainsectionRepository.GetAll().Where(u => u.ID == id).ToList();
        //    //LoMarket.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.ID_Login == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view

        //    return RedirectToAction("MainsectionView", new { id = id });
        //}
        public IActionResult WelcomTredMarket(int? id )
        {
            if (id == null)
            {
                // Handle the case where the id is null, for example:
                return RedirectToAction("Error");
            }

            LoginTredMarktViewModel LoMarket = new()
            {

                TredMarktVM = new Brands(),
                PreparatonLoginVM = new Preparations(),
                ProductionLoginVM = new Production(),
                FoodLoginVM = new FoodStuffs(),
                CleanLoginVM = new Cleaning(),
                DeviceToolsLoginVM = new DevicesAndTools(),
                ReadyFoodLoginVM = new ReadyProducts(),
                //tredList = new List<العلامة_التجارية>(),
                PreparatonLoginVMlist = new List<Preparations> (),
                FoodLoginVMlist = new List<FoodStuffs> (),
                CleanLoginVMlist = new   List<Cleaning> (),
                ProductionLoginVMlist = new   List<Production> (),
                ReadyFoodLoginVMlist = new   List<ReadyProducts> (),
                MainsectionVMlist = new List<MainSections>()
            };
            LoMarket.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            LoMarket.DeviceToolsLoginVM = _unitOfWork.Device_tools1.Get(u => u.BrandFK == id);
            LoMarket.ProductionLoginVM = _unitOfWork.itemsRepository.Get(u => u.BrandFK == id);
            LoMarket.CleanLoginVM = _unitOfWork.CleanRepository.Get(u => u.BrandFK == id);
            LoMarket.ReadyFoodLoginVM = _unitOfWork.readyFoodRepository.Get(u => u.BrandFK == id);
            LoMarket.FoodLoginVM = _unitOfWork.FoodRepository.Get(u => u.BrandFK == id);
            LoMarket.PreparatonLoginVM = _unitOfWork.PreparationRepository.Get(u => u.BrandFK == id);
            LoMarket.MainsectionVMlist = _unitOfWork.MainsectionRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            LoMarket.FoodLoginVMlist = _unitOfWork.FoodRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            LoMarket.ProductionLoginVMlist = _unitOfWork.itemsRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            LoMarket.PreparatonLoginVMlist = _unitOfWork.PreparationRepository.GetAll().Where(u=>u.BrandFK == id).ToList();
            LoMarket.ReadyFoodLoginVMlist = _unitOfWork.readyFoodRepository.GetAll().Where(u=>u.BrandFK == id).ToList();
            LoMarket.CleanLoginVMlist = _unitOfWork.CleanRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            ViewBag.IsAuthenticated = true;
            //LoMarket.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.ID_Login == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view

            // Populate the model

            return View(LoMarket);
            }

        public IActionResult MainsectionView(int? id)
        {
            if (id == null)
            {
                // Handle the case where the id is null, for example:
                return RedirectToAction("Error");
            }

            LoginTredMarktViewModel LoMarket = new()
            {

                TredMarktVM = new Brands(),
                //tredList = new List<العلامة_التجارية>(),
                PreparatonLoginVMlist = new List<Preparations>(),
                MainsectionVMlist = new List<MainSections>()
            };
            LoMarket.TredMarktVM = _unitOfWork.TredMarketRepository.Get(u => u.BrandID == id);
            LoMarket.MainsectionVMlist = _unitOfWork.MainsectionRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            LoMarket.PreparatonLoginVMlist = _unitOfWork.PreparationRepository.GetAll().Where(u => u.BrandFK == id).ToList();
            ViewBag.IsAuthenticated = true;
            //LoMarket.tredList = _unitOfWork.TredMarketRepository.GetAll().Where(c => c.ID_Login == id).ToList(); //هو يحتوي على قائمة من جدول المكونات واللي يساعده على العرض هي view

            // Populate the model

            return View(LoMarket);
        }
    }
}


//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Index(LoginModels model, string returnUrl = null)
//{


//    var user = _unitOfWork.loginRepository.Get(u => u.Username == model.Username );

//    if (user != null &&  _unitOfWork.loginRepository.UpdateUserPassword(user.ID, model.Password))
//    {

//        await _unitOfWork.loginRepository.VerifyUserCredentials(user.Username, model.Password);
//        return RedirectToLocal(returnUrl, user.ID); // Successful password update
//    }
//    else
//    {
//        ModelState.AddModelError(string.Empty, "خطأ في اسم المستخدم أو كلمة المرور ."); // Incorrect username or password
//        return View(model);
//    }

//}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public IActionResult Index(LoginModels model, string returnUrl = null)
//{
//    var user = _unitOfWork.loginRepository.Get(u => u.Username == model.Username);

//    if (user != null && _unitOfWork.loginRepository.VerifyUserCredentials(user.Username, model.Password))
//    {
//        // Username and password are correct, proceed with password update
//        if (_unitOfWork.loginRepository.UpdateUserPassword(user.ID, model.Password))
//        {
//            return RedirectToLocal(returnUrl, user.ID); // Successful password update
//        }
//        else
//        {
//            ModelState.AddModelError(string.Empty, "Failed to update password."); // Issue updating password
//            return View(model);
//        }
//    }
//    else
//    {
//        ModelState.AddModelError(string.Empty, "خطأ في اسم المستخدم أو كلمة المرور ."); // Incorrect username or password
//        return View(model);
//    }
//}




//[HttpPost]
//public IActionResult Index(LoginModels model, string returnUrl = null)
//{


//    var user = _unitOfWork.loginRepository.Get(u => u.Username == model.Username);
//    if (user != null && _unitOfWork.loginRepository.UpdateUserPassword(user.ID, model.Password))
//    {

//        return RedirectToLocal(returnUrl, user.ID);
//    }
//    else
//    {
//        ModelState.AddModelError(string.Empty, "اسم المستخدم أو كلمة المرور غير صالحة");
//        return View(model);
//    }

//}

//var user = _unitOfWork.loginRepository.Get(u => u.Username == model.Username);

//string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

//var storedUser = _unitOfWork.loginRepository.Get(u => u.Username == model.Username);
//if (storedUser != null)
//{
//    bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, hashedPassword);

//    if (isValidPassword)
//    {


//        // Authentication successful
//        // Perform the necessary tasks for authentication, e.g., set a session variable, authentication cookie, etc.
//        // Redirect the user to the requested URL or a default page
//        return RedirectToLocal(returnUrl, user.ID);
//    }
//    else
//    {
//        ModelState.AddModelError(string.Empty, "اسم المستخدم أو كلمة المرور غير صالحة ");
//        return View(model);
//    }
//}




//test1
//private readonly SignInManager<IdentityUser> _signInManager;
//private readonly ILogger<HomeController> _logger;

//public HomeController(SignInManager<IdentityUser> signInManager, ILogger<HomeController> logger)
//{
//    _signInManager = signInManager;
//    _logger = logger;
//}

//[HttpGet]
//public IActionResult Index(string returnUrl = null)
//{
//    ViewData["ReturnUrl"] = returnUrl;
//    var model = new LoginModels();
//    return View(model);
//}

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Index(LoginModels model, string returnUrl = null)
//    {
//        returnUrl ??= Url.Content("~/");

//        if (ModelState.IsValid)
//        {
//            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);

//            if (result.Succeeded)
//            {
//                _logger.LogInformation("User logged in.");
//                return LocalRedirect(returnUrl);
//            }
//            if (result.RequiresTwoFactor)
//            {
//                return RedirectToAction("LoginWith2fa", new { ReturnUrl = returnUrl });
//            }
//            if (result.IsLockedOut)
//            {
//                _logger.LogWarning("User account locked out.");
//                return RedirectToAction("Lockout");
//            }
//            else
//            {
//                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//                return View(model);
//            }
//        }

//        return View(model);
//    }
//}





//test2
//private readonly IUnitOfWork _unitOfWork;
//private readonly SignInManager<IdentityUser> _signInManager;

//public HomeController(IUnitOfWork unitOfWork, SignInManager<IdentityUser> signInManager)
//{
//    _unitOfWork = unitOfWork;
//    _signInManager = signInManager; 

//}

//public HomeController(ILogger<HomeController> logger)
//{
//    _logger = logger;
//}

//public IActionResult Index()
//{
//    return View();
//}

//[HttpPost]
//public IActionResult Index(Login1 login)
//{
//    if (ModelState.IsValid)
//    {
//        if (IsValidUser(login))
//        {
//            var claims = new List<Claim>
//    {
//        new Claim(ClaimTypes.Name, login.UserName),
//        // You can add other claims as needed
//    };

//            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

//            var authProperties = new AuthenticationProperties
//            {
//                IsPersistent = true, // You can set this based on your requirements
//            };

//            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

//            // Redirect to the dashboard or another protected page
//            return RedirectToAction("Dashboard");
//        }
//        else
//        {
//            ModelState.AddModelError(string.Empty, "Invalid username or password");
//            return View(login);
//        }
//    }

//    // If the ModelState is not valid, redisplay the login form with validation errors.
//    return View(login);
//}

//private bool IsValidUser(Login1 login)
//{
//    // Implement your authentication logic here by querying the database

//    var user = _unitOfWork.loginRepository.Get(u => u.UserName == login.UserName);

//    if (user != null)
//    {
//        // Verify the password using BCrypt
//        if (BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
//        {
//            return true;
//        }
//    }


//    return false;
//}


//[HttpPost]
//public async Task<IActionResult> Index(Login login)
//{
//    if (ModelState.IsValid)
//    {
//        var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, lockoutOnFailure: true);

//        if (result.Succeeded)
//        {
//            return RedirectToAction("Dashboard");
//        }
//        else
//        {
//            if (result.IsLockedOut)
//            {
//                ModelState.AddModelError(string.Empty, "Your account is locked due to too many failed attempts. Please try again later or reset your password.");
//            }
//            else if (result.IsNotAllowed)
//            {
//                ModelState.AddModelError(string.Empty, "Your account is not allowed to sign in. Please contact support for assistance.");
//            }
//            //else if (result.RequiresTwoFactor)
//            //{
//            //    // Handle two-factor authentication, if implemented
//            //    return RedirectToAction("TwoFactorVerification");
//            //}
//            else
//            {
//                ModelState.AddModelError(string.Empty, "Invalid username or password");
//            }

//            return View(login);
//        }
//    }

//    // If the ModelState is not valid, redisplay the login form with validation errors.
//    return View(login);
//}
//public IActionResult Privacy()
//    {
//        return View();
//    }

//    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//    public IActionResult Error()
//    {
//        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//    }
//}

//internal class HttppostAttribute : Attribute
//    {
//    }
