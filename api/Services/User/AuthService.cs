using api.Dtos.Auth;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class AuthService: IAuthService
{


    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    
    public AuthService(SignInManager<User> signInManager, UserManager<User>  userManager, ITokenService tokenService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }
    public async Task<NewUserDto?> Login(LoginDto loginDto)
    {
        try
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.Username);

            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return null;
            
            return new NewUserDto
            {
                Username = user.UserName!,
                Email = user.Email!,
                Token = _tokenService.CreateToken(user)
            };

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<NewUserDto?> Register(RegisterDto registerDto)
    {
        User user = new User
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
        };

        IdentityResult userResult = await _userManager.CreateAsync(user, registerDto.Password);

        if (!userResult.Succeeded) return null;

        IdentityResult rolesResult = await _userManager.AddToRoleAsync(user, "User");

        if (!rolesResult.Succeeded) return null;

        return new NewUserDto
        {
            Email = user.Email,
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }
}