using System.ComponentModel.DataAnnotations;

namespace PropertyManagementAPI.DTOs
{
    public class CityUpdateDTO
    {
        [Required]
        [StringLength(50,MinimumLength =2)]
        public required string CityName { get; set; }
      
    }
}
