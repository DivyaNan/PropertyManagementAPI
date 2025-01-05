using PropertyManagementAPI.Model;

namespace PropertyManagementAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
    }
}
