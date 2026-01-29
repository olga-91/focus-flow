using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[Authorize]
public class UserController(
    IUserService userService) : BaseController    
{
    [SwaggerOperation(Summary = "Creates a new user")]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(
        [FromBody] UserRegisterDto userRegisterDto)
    {
        var createdUser = await userService.RegisterAsync(userRegisterDto);

        return Ok(createdUser);
    }
    
    [SwaggerOperation(Summary = "Gets all users")]
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        var users = await userService.GetAllAsync();

        return Ok(users);
    }
}