using Test12.Models.Models.Login;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface ILoginRepository : IRepository<ClientLogin>
    {

        void Update(ClientLogin obj);
        //void AddUser();
        Task<bool> VerifyUserCredentialsWithExternalApi(string username, string password);
        bool UpdateUserPassword(int ID, string newPassword);
        void AddHashPassword(string username, string password);

        void UpdateLastLoginTime(int userId, DateTime lastLoginTime);
    }

}
