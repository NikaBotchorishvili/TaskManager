using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Dtos.TodoItem;

public class CreateTodoDto
{
    [Required]
    [MinLength(8)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(8)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [JsonPropertyName("due_date")]
    [DataType(DataType.DateTime)]
    public DateTime DueDate { get; set; }

    [Required]
    public bool Completed { get; set; } = false;
}