using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;
using Test12.DataAccess.RepositoryPro.IRepositoryPro1;
using Test12.Models.Models;
using Test12.Models.Models.Login;
using Test12.Models.Models.Production;

namespace Test12.DataAccess.Repository
{
    public class LoginReposiroty : Repository<LoginModels>, ILoginRepository
    {
        private readonly ApplicationDbContext _context;

        public LoginReposiroty(ApplicationDbContext context) : base(context)
        {
            _context = context;
           
        }

        public void Update(LoginModels obj)
        {


        }

        public async Task<bool> VerifyUserCredentialsWithExternalApi(string username, string password)
        {
            var user = await _context.LoginModels.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return false;
            }

            var passwordVerificationRequest = new
            {
                password = password,
                hash = user.Password
            };

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(passwordVerificationRequest), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://www.toptal.com/developers/bcrypt/api/check-password.json", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseString);
                    return result.is_correct;
                }
            }

            return false;
        }
        //public async Task<bool> VerifyUserCredentials(string username, string password)
        //{
        //    var user = await _context.LoginModels.FirstOrDefaultAsync(authUser => authUser.Username == username);

        //    if (user != null)
        //    {
        //        // Use BCrypt verify method to check if the provided password matches the hashed password
        //        return BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password);
        //    }
        //    return false;

        //}

        //public async Task<bool> VerifyUserCredentials(string username, string password)
        //{
        //    var user = await _context.LoginModels.FirstOrDefaultAsync(authUser => authUser.Username == username);

        //    if (user != null)
        //    {
        //        // Check password using the Bcrypt API
        //        string apiResponse = await SendCheckPasswordRequest(user.Password, password);

        //        return ParseApiResponse(apiResponse);
        //    }
        //    return false;
        //}

        //private async Task<string> SendCheckPasswordRequest(string hash, string password)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var content = new FormUrlEncodedContent(new[]
        //        {
        //    new KeyValuePair<string, string>("hash", hash),
        //    new KeyValuePair<string, string>("password", password)
        //});

        //        var response = await client.PostAsync("https://www.toptal.com/developers/bcrypt/api/check-password.json", content);
        //        var responseContent = await response.Content.ReadAsStringAsync();

        //        // Print the status code and response content to the console
        //        Console.WriteLine($"Status Code: {response.StatusCode}");
        //        Console.WriteLine($"Response Content: {responseContent}");

        //        return responseContent;
        //    }
        //}

        //private bool ParseApiResponse(string apiResponse)
        //{
        //    // Parse the JSON response from the API
        //    // and extract the "ok" value to determine
        //    // if the password matches the hash
        //    // Example using Newtonsoft.Json:
        //    var responseData = JObject.Parse(apiResponse);
        //    return (bool)responseData["ok"];
        //}

        public void UpdateLastLoginTime(int userId, DateTime lastLoginTime)
        {
            var user = _context.Brands.FirstOrDefault(u => u.BrandID == userId);
            if (user != null)
            {
                // Check if LastLoginTime is not set
                if (user.Date == null || user.Date == DateTime.MinValue)
                {
                    // Set a default value here before saving
                    user.Date = DateTime.UtcNow; // You can set any default value you prefer
                }
                else
                {
                    user.Date = lastLoginTime; // Update the last login time
                }

                _context.SaveChanges();
            }
        }
        //public void UpdateLastLoginTime(int userId, DateTime lastLoginTime)
        //{
        //    var user = _context.LoginModels.FirstOrDefault(u => u.ID == userId);
        //    if (user != null)
        //    {
        //        user.LastLoginTime = lastLoginTime; // Assuming LastLogin is a property in your user model to store last login time
        //        _context.SaveChanges();
        //    }
        //}


        //// Retrieve the user from the database based on the provided username
        //var user = _context.LoginModels.FirstOrDefault(u => u.Username == username);

        //if (user != null)
        //{
        //    // Compare the provided password with the stored hashed password
        //    return BCrypt.Net.BCrypt.Verify(password, user.Password);
        //}

        //// If the user is not found, or password doesn't match, return false
        //return false;

        // بعد تغيير كلمة المرور 
        public void AddHashPassword(string username, string password)
        {
            // Hash the user's password before storing it in the database
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, workFactor: 10);

            // Store the username and hashed password in the database
            var user = new LoginModels
            {
                Username = username,
                Password = hashedPassword
            };

            _context.Update(user);
            _context.SaveChanges();
        }

        //public bool VerifyPassword(int ID, int ID_tred, string username, string password)
        //{
        //    string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, workFactor: 13);
        //    // Store the username and hashed password in the database
        //    var user = new LoginModels
        //    {
        //        ID = ID,
        //        ID_tred = ID_tred,
        //        Username = username,
        //        Password = hashedPassword
        //    };

        //    _context.Update(user);

        //    // Retrieve the hashed password from the database based on the username
        //    var storedUser = _context.LoginModels.FirstOrDefault(u => u.Username == username);

        //    if (storedUser != null)
        //    {
        //        // Verify the provided password against the hashed password in the database
        //        return BCrypt.Net.BCrypt.EnhancedVerify(password, storedUser.Password);
        //    }

        //    return false; // User not found or password does not match
        //}

        public bool UpdateUserPassword(int ID, string newPassword)
        {
            var user = _context.LoginModels.FirstOrDefault(u => u.Login_ID == ID);

            if (user != null)
            {
                // Hash the new password before updating
                user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(newPassword, workFactor: 10);
                _context.SaveChanges();
                return true;
            }

            return false; // User not found or password not updated
        }

        public void hashPasswords()
        {
            var users = _context.LoginModels.ToList();

            foreach (var user in users)
            {
                if (!BCrypt.Net.BCrypt.EnhancedVerify(user.Password, user.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, workFactor: 10);
                }
            }

            _context.SaveChanges();
        }

    }
}
   
      
 
