using System.ComponentModel.DataAnnotations;

namespace PropertyManagementAPI.DTOs
{
    public class CityDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is mandatory")]
        [StringLength(50,MinimumLength =2)]
        public required string CityName { get; set; }
        public required string Country { get; set; }
    }
}
