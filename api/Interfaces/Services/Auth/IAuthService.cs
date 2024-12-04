using api.Dtos.Auth;
using api.Models;

namespace api.Interfaces;

public interface IAuthService
{
    Task<NewUserDto?> Login(LoginDto loginDto);
}