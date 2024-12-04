using api.Dtos.Auth;
using api.Interfaces;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;


[Route("api/auth")]
[ApiController]
public class AuthController: ControllerBase
{

    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    public AuthController(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register ([FromBody] RegisterDto registerDto){
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            IdentityResult userResult = await _userManager.CreateAsync(user, registerDto.Password);

            if (!userResult.Succeeded) return BadRequest(userResult.Errors);

            IdentityResult rolesResult = await _userManager.AddToRoleAsync(user, "User");

            if (!rolesResult.Succeeded) return BadRequest(rolesResult.Errors);

            return Ok(new NewUserDto
            {
                Email = user.Email,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            });

        }
        catch (Exception ex)
        {
            var errorDetails = new 
            {
                ex.Message, ex.StackTrace
            };
            return BadRequest(errorDetails);
        }
   
    }
}