using api.Dtos.Auth;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;


[Route("api/auth")]
[ApiController]
public class AuthController: ControllerBase
{

    private readonly UserManager<User> _userManager;
    public AuthController(UserManager<User> userManager)
    {
        _userManager = userManager;
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

            return Ok("User Registered Successfully");

        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
   
    }
}