using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

public class AuthController(
    IAuthService authService) : BaseController    
{
    [SwaggerOperation(Summary = "Authenticate user")]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserCredentialsDto userCredentialsDto)
    {
        var token = await authService.LoginAsync(userCredentialsDto);

        return Ok(new {token});
    }
    
    [SwaggerOperation(Summary = "Get the logged in user")]
    [Authorize]
    [HttpGet("user")]
    public IActionResult GetUser()
    {
        return Ok(CurrentUser);
    }
}