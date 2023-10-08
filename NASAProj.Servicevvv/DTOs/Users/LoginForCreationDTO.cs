using System.ComponentModel.DataAnnotations;

namespace NASAProj.Service.DTOs.Users
{
    public class LoginForCreationDTO
    {
        [EmailAddress, Required]
        public string Email { get; set; }

        [Required, MinLength(5)]
        public string Password { get; set; }
    }
}
