using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Dtos.Auth;

public class RegisterDto
{
    [Required]
    [MinLength(4)]
    [JsonPropertyName("username")]
    public required string Username { get; set; }
    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    [Required]
    [JsonPropertyName("password")]
    public required string Password { get; set; }
    
}