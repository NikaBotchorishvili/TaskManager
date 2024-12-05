using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.Interfaces;

namespace api.Services;

public class UserService: IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserIdFromClaims()
    {
        var user = _httpContextAccessor?.HttpContext?.User;

        if (user?.Identity is { IsAuthenticated: false })
        {
            Console.WriteLine("User is not authenticated.");
            return null;
        }

        var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            Console.WriteLine("UserId (sub claim) is not found.");
        }

        Console.WriteLine($"UserId: {userId}");
        return userId;
    }

    public string? GetUserNameFromClaims()
    {
        throw new NotImplementedException();
    }
}