using System.ComponentModel.DataAnnotations;

namespace PropertyManagementAPI.Model
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public required string CityName { get; set; }
        public required string Country {  get; set; }
        public DateTimeOffset LastUpdatedOn { get; set; }
        public int LastUpdatedBy{ get; set; }
    }
}
