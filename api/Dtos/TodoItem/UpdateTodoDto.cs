using System.ComponentModel.DataAnnotations;

namespace api.Dtos.TodoItem;

public class UpdateTodoDto
{
    [Required]
    [MinLength(8)]
    [MaxLength(100)]
    public string Title { get; set; }
    
    [Required]
    [MinLength(8)]
    public string Description { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DueDate { get; set; }

    [Required]
    public bool Completed { get; set; }
}