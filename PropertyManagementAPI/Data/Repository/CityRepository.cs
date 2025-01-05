using Microsoft.EntityFrameworkCore;
using PropertyManagementAPI.Interfaces;
using PropertyManagementAPI.Model;

namespace PropertyManagementAPI.Data.Repository
{
    public class CityRepository : ICityRepository
    {
      private readonly DataContext _dataContext;
        public CityRepository(DataContext datacontext)
        {
            this._dataContext = datacontext;   
        }
        public void AddCity(City city)
        {
            _dataContext.Cities.AddAsync(city);
        }

        public void DeleteCity(int CityId)
        {
           var city= _dataContext.Cities.Find(CityId);
            _dataContext.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
           return await _dataContext.Cities.ToListAsync();
        }

        public async Task<City> GetCitybyIdAsync(int CityId)
        {
            return await _dataContext.Cities.FindAsync(CityId);
        }

        //public async Task<IEnumerable<City>> UpdateCity(City city)
        //{
        //    _dataContext.Cities.Update(city);
        //}
    }
}
