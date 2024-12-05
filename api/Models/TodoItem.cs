using System.Text.Json.Serialization;

namespace api.Models;

public class TodoItem : BaseModel
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("due_date")]
    public DateTime DueDate { get; set; }

    [JsonPropertyName("completed")] 
    public bool Completed { get; set; } = false;
    
    
    public User User { get; set; }
    
    public string UserId { get; set; } = string.Empty;
}