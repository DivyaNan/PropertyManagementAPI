using PropertyManagementAPI.Data.Repository;
using PropertyManagementAPI.Interfaces;

namespace PropertyManagementAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public UnitOfWork(DataContext dataContext) {
        this._dataContext = dataContext;
        }

        public ICityRepository CityRepository => new CityRepository(_dataContext);

        public IUserRepository UserRepository =>  new UserRepository(_dataContext);

        
        public async Task<bool> SaveAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
