using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NASAProj.Controllers;
using NASAProj.Domain.Configurations;
using NASAProj.Service.DTOs.Users;
using NASAProj.Service.Extensions;
using NASAProj.Service.Interfaces;
using ZaminEducation.Api.Extensions.Attributes;
using ZaminEducation.Api.Helpers;

namespace ZaminEducation.Api.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="dto">user create</param>
    /// <returns>Created user infortaions</returns>
    /// <response code="200">If user is created successfully</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> CreateAsync(UserForCreationDTO dto) =>
        Ok(await userService.CreateAsync(dto));


    /// <summary>
    /// Delete user by id (for only admins)
    /// </summary>
    /// <param name="id"></param>
    /// <returns>true if user deleted succesfully else false</returns>
    [HttpDelete("{id}"), Authorize]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id) =>
        Ok(await userService.DeleteAsync(user => user.Id == id));


    /// <summary>
    /// Get all of users
    /// </summary>
    /// <param name="params">pagenation params</param>
    /// <returns> user collection </returns>
    [HttpGet, Authorize]
    public async ValueTask<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params) =>
            Ok(await userService.GetAllAsync(@params));

    /// <summary>
    /// Update password
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("password"), Authorize]
    public async ValueTask<IActionResult> ChangePasswordAsync(UserForChangePasswordDTO dto) =>
        Ok(await userService.ChangePasswordAsync(dto));

    /// <summary>
    /// Get one user information
    /// </summary>
    /// <param name="id">user id</param>
    /// <returns>user</returns>
    /// <response code="400">if user data is not in the base</response>
    /// <response code="200">if user data have in database</response>
    [HttpGet("{id}"), Authorize]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id) =>
        Ok(await userService.GetAsync(user => user.Id == id));

    /// <summary>
    /// Update user 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut, Authorize]
    public async ValueTask<IActionResult> UpdateAsync(
        int id, [FromBody] UserForUpdateDTO dto) =>
            Ok(await userService.UpdateAsync(id, dto));

    /// <summary>
    /// Get self user info
    /// </summary>
    /// <returns>user</returns>
    [HttpGet("info"), Authorize]
    public async ValueTask<IActionResult> GetInfoAsync()
        => Ok(await userService.GetInfoAsync());

    /// <summary>
    /// Create attachment
    /// </summary>
    /// <returns></returns>
    [HttpPost("attachments/{id}"), Authorize]
    public async Task<IActionResult> Attachment(int id, IFormFile formFile)
        => Ok(await userService.AddAttachmentAsync(id, formFile.ToAttachmentOrDefault()));
}