using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NASAProj.Service.DTOs.Users;
using NASAProj.Service.Interfaces;
using ZaminEducation.Service.DTOs.Users;
using ZaminEducation.Service.Interfaces;

namespace NASAProj.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] UserForCreationDTO userDto)
            => Ok(await userService.CreateAsync(userDto));


        [HttpPost("Login"), AllowAnonymous]
        public async Task<IActionResult> Login(LoginForCreationDTO loginDto)
            => Ok(await authService.GenerateToken(loginDto.Email, loginDto.Password));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
            => Ok(await userService.GetAsync(p => p.Id == id));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromForm] UserForUpdateDTO userDto)
            => Ok(await userService.UpdateAsync(id, userDto));

        [HttpPatch("Password")]
        public async Task<IActionResult> UpdatePassword(UserForChangePasswordDTO passwordDto)
            => Ok(await userService.ChangePasswordAsync(passwordDto));
    }
}

