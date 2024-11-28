using System.Text.Json.Serialization;

namespace api.Models;

public class Task : BaseModel
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("due_date")]
    public DateTime DueDate { get; set; }
}