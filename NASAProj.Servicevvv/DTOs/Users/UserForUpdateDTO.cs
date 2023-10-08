using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NASAProj.Service.DTOs.Users
{
    public class UserForUpdateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
    }
}
