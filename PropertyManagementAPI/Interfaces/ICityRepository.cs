using PropertyManagementAPI.Model;

namespace PropertyManagementAPI.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City city);
        void DeleteCity(int CityId);
   
        Task<City> GetCitybyIdAsync(int CityId);
       // Task<IEnumerable<City>> UpdateCity(City city);
    }
}
