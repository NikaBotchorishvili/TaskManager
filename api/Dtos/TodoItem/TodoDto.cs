using System.Text.Json.Serialization;

namespace api.Dtos.TodoItem;

public class TodoDto: BaseDto
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("due_date")]
    public DateTime DueDate { get; set; }

    [JsonPropertyName("completed")] 
    public bool Completed { get; set; }
}