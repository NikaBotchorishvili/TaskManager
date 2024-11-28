using System.Text.Json.Serialization;

namespace api.Models;

public class BaseModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdateAt { get; set; }
}