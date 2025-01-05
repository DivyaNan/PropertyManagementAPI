using Microsoft.EntityFrameworkCore;
using PropertyManagementAPI.Interfaces;
using PropertyManagementAPI.Model;

namespace PropertyManagementAPI.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x=>x.UserName==username
                                                                && x.Password == password);
        }
    }
}
