namespace api.Interfaces;

public interface IUserService
{
    string? GetUserIdFromClaims();
    string? GetUserNameFromClaims();
}