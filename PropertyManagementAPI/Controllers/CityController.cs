using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PropertyManagementAPI.Data.Repository;
using PropertyManagementAPI.DTOs;
using PropertyManagementAPI.Interfaces;
using PropertyManagementAPI.Model;

namespace PropertyManagementAPI.Controllers
{
    [Authorize]
    public class CityController : BaseController
    {
 
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CityController(IUnitOfWork uow,IMapper mapper)
        {
          
            this._uow = uow;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            //throw new UnauthorizedAccessException("unauthorsied");
            var cities = await _uow.CityRepository.GetCitiesAsync();
            //var citiesDTO = from c in cities
            //                select new CityDTO
            //              { CityName=c.CityName,
            //              Id=c.Id,
            //                };
            var citiesDTO=_mapper.Map<IEnumerable<CityDTO>>(cities);
            return Ok(citiesDTO);
        }
        [HttpPost("add")]
        [HttpPost("add/{cityName}")]
        [HttpPost("post")]
        //  public async Task<IActionResult> AddCity(string cityName)
        public async Task<IActionResult> AddCity(CityDTO cityDTO)
            {
            //City city = new City() { CityName=cityName};

            //var cityToAdd = new City
            //{
            //    CityName = city.CityName,
            //    LastUpdatedBy = 1,
            //    LastUpdatedOn = DateTime.UtcNow
            //};

            var cityToAdd = _mapper.Map<City>(cityDTO);
            cityToAdd.LastUpdatedOn = DateTime.UtcNow;
            cityToAdd.LastUpdatedBy = 1;
            _uow.CityRepository.AddCity(cityToAdd);
            await _uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int Id,CityDTO cityDTO)
        {
            
            var cityToUpdate = await _uow.CityRepository.GetCitybyIdAsync(Id);
            if( cityToUpdate is null)
                return NotFound();
            cityToUpdate.LastUpdatedOn = DateTime.UtcNow;
            cityToUpdate.LastUpdatedBy = 1;
             _mapper.Map(cityDTO,cityToUpdate);
            await _uow.SaveAsync();
            return StatusCode(204);
        }


        [HttpPut("updateCityName/{id}")]
        public async Task<IActionResult> UpdateCityName(int Id, CityUpdateDTO cityDTO)
        {

            var cityToUpdate = await _uow.CityRepository.GetCitybyIdAsync(Id);
            if (cityToUpdate is null)
                return NotFound();
            cityToUpdate.LastUpdatedOn = DateTime.UtcNow;
            cityToUpdate.LastUpdatedBy = 1;
            _mapper.Map(cityDTO, cityToUpdate);
            await _uow.SaveAsync();
            return StatusCode(204);
        }
        [HttpPatch("updateCityPatch/{id}")]
        public async Task<IActionResult> UpdateCityPatch(int Id, JsonPatchDocument<City> cityToPatch)
        {

            var cityToUpdate = await _uow.CityRepository.GetCitybyIdAsync(Id);
            if (cityToUpdate is null)
                return NotFound();
            cityToUpdate.LastUpdatedOn = DateTime.UtcNow;
            cityToUpdate.LastUpdatedBy = 1;
            cityToPatch.ApplyTo(cityToUpdate, ModelState);
            await _uow.SaveAsync();
            return StatusCode(204);
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteCity(int Id)
        {
            var citytobedeleted = await _uow.CityRepository.GetCitybyIdAsync(Id);
            if (citytobedeleted is null)
            {
                return NotFound();
            }

            _uow.CityRepository.DeleteCity(Id);
            await _uow.SaveAsync(); ;
            return Ok(Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id)
        {
            var city = await _uow.CityRepository.GetCitybyIdAsync(id);
            if(city is null)
            {
                return NotFound();
;            }
            //var cityDTO = new CityDTO { CityName=city.CityName,Id=city.Id};
            var cityDTO = _mapper.Map<CityDTO>(city);
            return Ok(cityDTO);
        }
    }
}
