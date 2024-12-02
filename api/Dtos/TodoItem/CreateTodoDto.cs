using System.ComponentModel.DataAnnotations;

namespace api.Dtos.TodoItem;

public class CreateTodoDto
{
    [Required]
    [MinLength(8)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(8)]
    public string Description { get; set; } = string.Empty;
    
    [DataType(DataType.DateTime)]
    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public bool Completed { get; set; } = false;
}