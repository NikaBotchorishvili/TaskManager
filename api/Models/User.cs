using System.Text.Json.Serialization;
namespace api.Models;

public class User: BaseModel
{
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
}