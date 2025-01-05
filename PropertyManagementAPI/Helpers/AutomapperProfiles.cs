using AutoMapper;
using PropertyManagementAPI.DTOs;
using PropertyManagementAPI.Model;

namespace PropertyManagementAPI.Helpers
{
    public class AutomapperProfiles:Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<City,CityDTO>().ReverseMap();
            CreateMap<City, CityUpdateDTO>().ReverseMap();

        }
    }
}
