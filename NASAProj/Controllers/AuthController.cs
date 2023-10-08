using Microsoft.AspNetCore.Mvc;
using NASAProj.Controllers;
using NASAProj.Service.DTOs.Users;
using NASAProj.Service.Interfaces;

namespace ZaminEducation.Api.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        /// <summary>
        /// Authorization
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginForCreationDTO dto)
        {
            var token = await authService.GenerateToken(dto.Email, dto.Password);
            return Ok(new
            {
                token
            });
        }
    }
}
