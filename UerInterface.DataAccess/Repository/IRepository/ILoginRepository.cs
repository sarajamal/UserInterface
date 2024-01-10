using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test12.Models.Models.Login;
using Test12.Models.Models.Production;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface ILoginRepository : IRepository<LoginModels>
    {

        void Update(LoginModels obj);
        //void AddUser();
        Task<bool> VerifyUserCredentialsWithExternalApi(string username, string password);
        bool UpdateUserPassword(int ID, string newPassword);
        void AddHashPassword(string username, string password);

        void UpdateLastLoginTime(int userId, DateTime lastLoginTime);
    }

}
