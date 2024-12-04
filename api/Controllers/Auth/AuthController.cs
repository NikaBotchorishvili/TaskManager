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
    private readonly IAuthService _authService;
    public AuthController(UserManager<User> userManager, ITokenService tokenService, IAuthService authService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _authService = authService;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userResponse = await (_authService.Login(loginDto));

        if (userResponse == null) return Unauthorized();
        
        return Ok(userResponse);
    }
}