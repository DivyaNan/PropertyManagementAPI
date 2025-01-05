using System.ComponentModel.DataAnnotations;

namespace PropertyManagementAPI.DTOs
{
    public class LoginReqDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
